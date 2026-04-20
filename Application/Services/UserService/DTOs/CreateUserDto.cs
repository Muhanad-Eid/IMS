using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.UserService.DTOs
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; } = 0;


    }
}
