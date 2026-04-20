using Application.Services.UserService.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<GetUserDto>> GetUsers(string? name, string? email); 
        public Task<GetUserDto> GetUser(int id);
        public Task CreateUser(AddUserDto user);
        public Task UpdateUser(int id, UpdateUserDto user);
        public Task DeleteUser(int id);
    }
}
