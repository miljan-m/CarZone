using AutoMapper;
using CarZone.Application.DTOs.UserDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Domain.Models;

namespace CarZone.Application.Services
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository _repository;
        protected readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<GetUserDTO> GetUserById(int userId)
        {
            var user = await _repository.GetById(userId);
            if (user == null) return null;
            return _mapper.Map<GetUserDTO>(user);
        }

        public async Task<IEnumerable<GetUserDTO>> GetAllUsers()
        {
            var users = await _repository.GetAll();
            return users.Select(s => _mapper.Map<GetUserDTO>(s));
        }

        public async Task<GetUserDTO> CreateUser(CreateUserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            var createdUser = await _repository.Create(_mapper.Map<User>(userDTO));
            return _mapper.Map<GetUserDTO>(createdUser);
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var isDeleted = await _repository.Delete(userId);
            return isDeleted;
        }

        public async Task<GetUserDTO> UpdateUser(int userId, UpdateUserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            var isUpdated = await _repository.Update(userId, _mapper.Map<User>(userDTO));
            var u = await _repository.GetById(userId);
            return _mapper.Map<GetUserDTO>(u);
        }

    }
}