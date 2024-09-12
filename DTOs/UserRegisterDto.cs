using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NemuraProject.DTOs;

public class UserRegisterDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
