using CarZone.Application.DTOs.UserDTOs;
using CarZone.Domain.Models;

namespace CarZone.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetUserByEmailAndPassword(string email,string password);
    }
}