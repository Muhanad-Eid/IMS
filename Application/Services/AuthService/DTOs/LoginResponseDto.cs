using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.AuthService.DTOs
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public SystemRole RoleCode { get; set; } 
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
