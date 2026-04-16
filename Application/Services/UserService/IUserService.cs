using Application.Services.UserService.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        public List<GetUserDto> GetUsers(string? name, string? email); 
    }
}
