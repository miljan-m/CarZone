using CarZone.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using CarZone.Infrastructure.Persistance;
using CarZone.Application.Interfaces.Repositories;

namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> _repository;
        public UserController(IGenericRepository<User> repository)
        {
            _repository=repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser([FromRoute]int id)
        {
            var user=await _repository.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _repository.Create(user);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users=await _repository.GetAll();
            return Ok(users);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute]int id)
        {
            await _repository.Delete(id);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute]int id,[FromBody]User user)
        {
            await _repository.Update(id,user);
            return Ok();
        }
    }
}