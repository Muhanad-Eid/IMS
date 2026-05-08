using Application.Services.AuthService.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.AuthService.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto input);
        Task ChangeUserPassword(ChangeUserPasswordDto input);
        Task<string> RefreshToken(RefreshTokenDto input);
        Task Logout();
    }
}
