using EvalApi.Src.Models;  // Pour accéder à UserModel
using EvalApi.Src.Core.Services;
using EvalApi.Src.Views.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EvalApi.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid user data.");
            }

            // Mapper CreateUserDto à UserModel
            var userModel = new UserModel
            {
                Name = userDto.Name,
                Username = userDto.Username,
                Email = userDto.Email
            };

            var createdUser = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetAllUsers), new { id = createdUser.Id }, createdUser);
        }
         [HttpGet("error")]
    public IActionResult ThrowError()
    {
        throw new Exception("Ceci est une erreur test.");
    }
    }
}
