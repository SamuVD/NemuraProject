using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using NemuraProject.DataBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from a .env file.
Env.Load();

// Add environment variables to the project's configuration system.
builder.Configuration.AddEnvironmentVariables();

// Here we get the environment variables to connect to the database.
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbDatabaseName = Environment.GetEnvironmentVariable("DB_DATABASE");
var dbUser = Environment.GetEnvironmentVariable("DB_USERNAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

// We build the MySQL connection string using the environment variables.
var mySqlConnection = $"server={dbHost};port={dbPort};database={dbDatabaseName};uid={dbUser};password={dbPassword}";

// We register the database context (DbContext) in the project's services.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.Parse("8.0.20-mysql")));

// Get the necessary environment variables to configure JWT authentication.
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
var jwtExpireMinutes = Environment.GetEnvironmentVariable("JWT_EXPIREMINUTES");

// Configure JWT authentication in the project's services.
// It is specified that the JWT authentication scheme will be used to authenticate and challenge requests.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Specific JWT configuration, that includes validations such as issuer, audience, token lifetime, and the security key used to sign the tokens.
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;  // HTTPS is required for the token.
    options.SaveToken = true;  // Saves the authentication token.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,  // Validates that the token issuer is correct.
        ValidateAudience = true,  // Validates that the token audience is correct.
        ValidateLifetime = true,  // Validates that the token is not expired.
        ValidateIssuerSigningKey = true,  // Validates that the signing key used to sign the token is valid.
        ValidIssuer = jwtIssuer,   // Defines the valid issuer.
        ValidAudience = jwtAudience,  // Defines the valid audience.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))  // Defines the token signing key.
    };
});

// We add the authorization service, that will be used to restrict access to certain endpoints.
builder.Services.AddAuthorization();

// We configure CORS (Cross-Origin Resource Sharing) policies.
// This allows only certain origins (domains) to make requests to our API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", // Name of CORS policy
        builder =>
        {
            // Allows any type of header and HTTP method (GET, POST, etc.).
            builder.WithOrigins("http://127.0.0.1:5173", "http://localhost:5173", "https://appnemura.netlify.app")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Allows credentials (cookies, authentication headers) to be sent.
        });
});

// We add support for controllers (MVC or API Controllers).
// This allows the application to handle HTTP requests and return responses using controllers.
builder.Services.AddControllers();

// We configure Swagger, a tool that generates interactive API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Here Swagger is configured in the pipeline, allowing users to view the API documentation.
app.UseSwagger();
app.UseSwaggerUI();

// Configure CORS so that the previously defined policies apply to requests.
app.UseCors("AllowSpecificOrigin");

// Enable authentication in the application's pipeline, meaning each request will go through the JWT authentication process.
app.UseAuthentication();

// Enable authorization, that will be used to ensure that only users with permissions can access certain resources.
app.UseAuthorization();

// Map the controllers defined in the API to respond to HTTP routes.
app.MapControllers();

// Run the application, starting the server and listening for requests.
app.Run();