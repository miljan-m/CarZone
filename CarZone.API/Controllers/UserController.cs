using CarZone.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using CarZone.Application.DTOs.UserDTOs;
using CarZone.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;


namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<GetUserDTO>> GetUser([FromRoute] int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO user)
        {
            await _userService.CreateUser(user);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }


        [HttpDelete("{userId}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            var isDeleted = await _userService.DeleteUser(userId);
            if (isDeleted) return Ok();
            return NotFound();
        }


        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UpdateUserDTO dto)
        {
            await _userService.UpdateUser(userId, dto);
            return Ok();
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<GetLoginUserDTO>> Login([FromBody] LoginUserDTO loginDTO)
        {
            var user = await _userService.Login(loginDTO.Email, loginDTO.Password);
            if (user == null) return Unauthorized();
            return Ok(user);
        }
    }
}