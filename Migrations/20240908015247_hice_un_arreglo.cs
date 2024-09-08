using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NemuraProject.Migrations
{
    /// <inheritdoc />
    public partial class hice_un_arreglo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "timer",
                table: "assignments",
                newName: "stopwatch");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stopwatch",
                table: "assignments",
                newName: "timer");
        }
    }
}
