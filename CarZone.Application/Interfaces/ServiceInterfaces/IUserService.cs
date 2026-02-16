using CarZone.Application.DTOs.UserDTOs;

namespace CarZone.Application.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        public Task<GetUserDTO> GetUserById(int userId);
        public Task<IEnumerable<GetUserDTO>> GetAllUsers();
        public Task<bool> DeleteUser(int userId);
        public Task<GetUserDTO> CreateUser(CreateUserDTO user); 
        public Task<GetUserDTO> UpdateUser(int id,UpdateUserDTO user);

        public Task<GetLoginUserDTO> Login(string email,string password);
    }
}