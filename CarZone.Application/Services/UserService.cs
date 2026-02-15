using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CarZone.Application.DTOs.UserDTOs;
using CarZone.Application.Interfaces;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Application.Interfaces.ServiceInterfaces.ISecurity;
using CarZone.Application.Validation.CreateValidation;
using CarZone.Application.Validation.UpdateValidation;
using CarZone.Domain.Models;

namespace CarZone.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _provider;
        private readonly IPasswordHash _passwordHasher;
        public UserService(IUserRepository repository, IMapper mapper, IJwtProvider provider, IPasswordHash passwordHasher)
        {
            _repository = repository;
            _mapper = mapper;
            _provider = provider;
            _passwordHasher = passwordHasher;
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
            var validator = new CreateUserDTOValidator();
            var result = validator.Validate(userDTO);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
                throw new ValidationException("User data is invalid");
            }
            var password = _passwordHasher.HashPassword(userDTO, userDTO.Password);
            userDTO.Password = password;
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
            var validator = new UpdateUserDTOValidator();
            var result = validator.Validate(userDTO);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Validation eerror: {error.ErrorMessage}");
                }
                throw new ValidationException("User data is invalid");
            }
            var isUpdated = await _repository.Update(userId, _mapper.Map<User>(userDTO));
            var u = await _repository.GetById(userId);
            return _mapper.Map<GetUserDTO>(u);
        }

        public async Task<GetLoginUserDTO> Login(string email, string password)
        {
            var user = await _repository.GetUserByEmailAndPassword(email, password);
            if (user == null) return null;
            var passwordOk = _passwordHasher.VerifyPassword(user, user.HashPassword, password);
            if (passwordOk == false) return null;

            var token = _provider.Generate(user);
            return new GetLoginUserDTO
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Token = token
            };
        }

    }
}