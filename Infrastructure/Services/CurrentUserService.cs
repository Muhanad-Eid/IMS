using Application.Services.CurrentUser;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int? UserId { get => Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value); }
        public string? Name { get => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value; }
        public string? Email { get => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value; }
        public string? MobilePhone { get => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.MobilePhone)?.Value; }
        public string? Role { get => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value; }
    }
}
