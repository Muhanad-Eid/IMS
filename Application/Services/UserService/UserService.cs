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
        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(AddUserDto user)
        {
            var newUser = new User
            {
                Id = 0,
                Name = user.Name,
                PhoneNumber = user.Phone,
                Email = user.Email,
                RoleId = user.RoleId,
                Password = user.Passsword
            };
            _userRepository.Add(newUser);
        }

        public List<GetUserDto> GetUsers(string? name,string? email)
        {
            name = !string.IsNullOrEmpty(name) ? name.ToLower().Trim() : null;
            var users = new List<GetUserDto>();
            if(name==null)
                users = _userRepository.GetAll().Select(u => new GetUserDto { Id = u.Id, Name = u.Name, Email = u.Email }).ToList();
            else
                users = _userRepository.GetAll().Select(u => new GetUserDto { Id = u.Id, Name = u.Name, Email = u.Email }).ToList().Where(u=>u.Name.ToLower().Contains(name)).ToList();
            return users;
        }
        public GetUserDto GetUser(int id)
        {
            var user = _userRepository.GetById(id);
            var x = new GetUserDto { Email = user.Email, Name = user.Name, PhoneNumber = user.PhoneNumber ,Id=user.Id};
            return x;
        }

        
    }
}
