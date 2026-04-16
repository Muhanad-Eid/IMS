using System;
using System.Collections.Generic;
using System.Text;
using Application.Repositories;
using Application.Services.Interfaces;
using Application.Services.UserService.DTOs;
using Domain.Entities;
namespace Application.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly IGenericRepository<User> _userRepository;
        public UserService(IGenericRepository<User> genericRepository)
        {
            _userRepository = genericRepository;
        }
        public List<GetUserDto> GetUsers(string? name, string? email)
        {
            return new List<>();
        }

        
    }
}
