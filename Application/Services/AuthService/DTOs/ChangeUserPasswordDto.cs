using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.AuthService.DTOs
{
    public class ChangeUserPasswordDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
