using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.UserService.DTOs
{
    public class AddUserDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Passsword { get; set; }
        public int RoleId { get; set; } = 0;


    }
}
