using System.Runtime.CompilerServices;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CarZone.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CarZoneDBContext _context;
        public GenericRepository(CarZoneDBContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T obj)
        {
            if (obj == null) return null;
            await _context.Set<T>().AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _context.Set<T>().FindAsync(id);
            if (obj == null) return false;
            _context.Set<T>().Remove(obj);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var obj = await _context.Set<T>().FindAsync(id);
            if (obj == null) return null;
            return obj;
        }

        public async Task<bool> Update(int id, T obj)
        {
            var dbObj = await _context.Set<T>().FindAsync(id);
            if (dbObj == null) return false;

            var entry = _context.Entry(dbObj);

            foreach (var property in entry.Properties)
            {
                if (property.Metadata.IsPrimaryKey())
                    continue;

                property.CurrentValue =
                    typeof(T).GetProperty(property.Metadata.Name)
                             ?.GetValue(obj);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}