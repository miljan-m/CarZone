using CarZone.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.DTOs.UserDTOs;
using CarZone.Application.Mappers;
using AutoMapper;


namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;
        public UserController(IGenericRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> GetUser([FromRoute] int id)
        {
            var user = await _repository.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(_mapper.Map<GetUserDTO>(user));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO user)
        {
            var dto = _mapper.Map<User>(user);
            await _repository.Create(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _repository.GetAll();
            var mappedUsers = users.Select(u => _mapper.Map<GetUserDTO>(u));
            return Ok(mappedUsers);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await _repository.Delete(id);
            return Ok();
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDTO dto)
        {
            await _repository.Update(id, _mapper.Map<User>(dto));
            return Ok();
        }
    }
}