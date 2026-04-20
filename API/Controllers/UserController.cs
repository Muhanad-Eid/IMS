using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Repositories;
using Application.Services.Interfaces;
using Application.Services.UserService.DTOs;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(string?name, string?email)
        {
            var users = _userService.GetUsers(name, email);
            return Ok(users);
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            return Ok(user);
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {
            await _userService.CreateUser(user);
            return Ok();
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto user)
        {
            await _userService.UpdateUser(id, user);
            return Ok();
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }
    }
}
