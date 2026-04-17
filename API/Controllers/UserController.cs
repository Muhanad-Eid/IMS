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
        public IActionResult GetUsers(string?name, string?email)
        {
            var users = _userService.GetUsers(name, email);
            return Ok(users);
        }
        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUser(id);
            return Ok(user);
        }
        [HttpPost]
        public IActionResult AddUser(AddUserDto user)
        {
            _userService.AddUser(user);
            return Ok();
        }
    }
}
