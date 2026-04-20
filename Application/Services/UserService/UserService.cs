using System;
using System.Collections.Generic;
using System.Text;
using Application.Repositories;
using Application.Services.Interfaces;
using Application.Services.UserService.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Application.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly IGenericRepository<User> _userRepository;
        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<List<GetUserDto>> GetUsers(string? name,string? email)
        {
            name = !string.IsNullOrEmpty(name) ? name.ToLower().Trim() : null;
            var users = _userRepository.GetAll();
            if(name!=null)  
                users = users.Where(u=>u.Name.ToLower().Contains(name));
            if(email!=null)  
                users = users.Where(u=>u.Email.ToLower().Contains(email));
            users = users.Include(u => u.Role);
            var result = users.Select(u => new GetUserDto { Id = u.Id, Name = u.Name, Email = u.Email ,RoleName=u.Role.Name,PhoneNumber=u.PhoneNumber}).ToList();
            return result;
        }
        public async Task<GetUserDto> GetUser(int id)
        {
            var data =await _userRepository.GetByIdAsync(id);
            var user = new GetUserDto 
            { 
                Email = data.Email, 
                Name = data.Name, 
                PhoneNumber = data.PhoneNumber ,
                Id=data.Id,
                RoleId=data.RoleId,
            };
            return user;
        }
        public async Task CreateUser(AddUserDto user)
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
            await _userRepository.CreateAsync(newUser);
            await _userRepository.SaveChangesAsync();
        }
        public async Task UpdateUser(int id, UpdateUserDto user)
        {
            var data = await _userRepository.GetByIdAsync(id);
            var newUser = new User { Email= user.Email, Name = user.Name, PhoneNumber = user.PhoneNumber, RoleId = data.RoleId, Password = data.Password };
            await _userRepository.UpdateAsync(newUser);
            await _userRepository.SaveChangesAsync();
        }
        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();
        }

    }
}
