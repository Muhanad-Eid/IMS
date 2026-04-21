using System;
using System.Collections.Generic;
using System.Text;
using Application.Repositories;
using Application.Services.Interfaces;
using Application.Services.UserService.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
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


        public List<GetListDto> GetUsers(string? name,string? email)
        {
            name = !string.IsNullOrEmpty(name) ? name.ToLower().Trim() : null;
            var users = _userRepository.GetAll();
            if(name!=null)  
                users = users.Where(u=>u.Name.ToLower().Contains(name));
            if(email!=null)  
                users = users.Where(u=>u.Email.ToLower().Contains(email));
            users = users.Include(u => u.Role);
            var result = users.Select(u => new GetListDto { Id = u.Id, Name = u.Name, Email = u.Email ,PhoneNumber=u.PhoneNumber,RoleName=u.Role.Name}).ToList();
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
        public async Task CreateUser(CreateUserDto user)
        {
            if(await _userRepository.GetAll().AnyAsync(u=>u.Email==user.Email.ToLower().Trim()))
                throw new Exception("Email already exists");
            if(await _userRepository.GetAll().AnyAsync(u=>u.PhoneNumber==user.Phone.Trim()))
                throw new Exception("Phone number already exists");
            var newUser = new User
            {
                Id = 0,
                Name = user.Name,
                PhoneNumber = user.Phone.Trim(),
                Email = user.Email.ToLower().Trim(),
                RoleId = user.RoleId,
                
            };
            var passwordHasher = new PasswordHasher<User>();
            newUser.Password = passwordHasher.HashPassword(newUser, user.Password);  

            await _userRepository.CreateAsync(newUser);
            await _userRepository.SaveChangesAsync();
        }
        public async Task UpdateUser(int id, UpdateUserDto user)
        {
            if (await _userRepository.GetAll().AnyAsync(u => u.Email == user.Email.ToLower().Trim() && u.Id != id))//id is to check if the email belongs to the same user or not
                throw new Exception("Email already exists");
            if (await _userRepository.GetAll().AnyAsync(u => u.PhoneNumber == user.PhoneNumber.Trim()&& u.Id != id))//id is to check if the phone number belongs to the same user or not
                throw new Exception("Phone number already exists");
            var data = await _userRepository.GetByIdAsync(id);
            data.Name = user.Name;
            data.Email = user.Email;
            data.RoleId = user.RoleId;
            data.Email = user.Email;
            
            _userRepository.Update(data);
            await _userRepository.SaveChangesAsync();
        }
        public async Task DeleteUser(int id)
        {
            var data =await _userRepository.GetByIdAsync(id);
            _userRepository.Delete(data);
            await _userRepository.SaveChangesAsync();
        }

    }
}
