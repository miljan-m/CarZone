using CarZone.Application.DTOs.UserDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Domain.Models;
using CarZone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CarZone.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly CarZoneDBContext _context;
        protected readonly DbSet<User> _dbSet;

        public UserRepository(CarZoneDBContext context)
        {
            _context = context;
            _dbSet=_context.Set<User>();
        }

        public async Task<User> Create(User obj, int id = int.MinValue)
        {   
            await _dbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;

        }

        public async Task<bool> Delete(int id)
        {
            var userToDelete = await _dbSet.FindAsync(id);
            if (userToDelete == null) return false;
            _dbSet.Remove(userToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            var user = await _dbSet.FirstAsync(u => u.Email == email && u.Password == password);
            if(user==null) return null;
            return user;
        }

        public async Task<User> GetById(int id)
        {
            var v= await _dbSet.FindAsync(id);
            return v;
        }

        public async Task<bool> Update(int id, User obj)
        {
            var user = await _dbSet.FindAsync(id);
            if (user == null) return false;

            var entry = _dbSet.Entry(user);
            foreach (var property in entry.Properties)
            {
                if (property.Metadata.IsPrimaryKey()) continue;
                if (property.Metadata.IsForeignKey()) continue;

                var propInfo = typeof(User).GetProperty(property.Metadata.Name);
                if (propInfo == null) continue;

                var newValue = propInfo.GetValue(obj);
                if(newValue==null) continue;
                property.CurrentValue = newValue;

            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}