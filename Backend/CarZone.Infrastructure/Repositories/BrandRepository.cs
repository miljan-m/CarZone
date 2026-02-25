using CarZone.Application.Interfaces.Repositories;
using CarZone.Domain.Models;
using CarZone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CarZone.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        protected readonly CarZoneDBContext _context;
        protected readonly DbSet<Brand> _dbSet;

        public BrandRepository(CarZoneDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<Brand>();
        }



        public async Task<Brand> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _dbSet.ToListAsync();

        }

        public async Task<Brand> Create(Brand obj, int id = int.MinValue)
        {
            await _dbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> Update(int id, Brand obj)
        {
            var brand = await _dbSet.FindAsync(id);
            if (brand == null) return false;

            var properties = _dbSet.Entry(brand).Properties;

            foreach (var prop in properties)
            {
                if (prop.Metadata.IsPrimaryKey()) continue;
                if (prop.Metadata.IsForeignKey()) continue;

                var propInfo = typeof(Brand).GetProperty(prop.Metadata.Name);
                if (propInfo == null) continue;

                var newValue = propInfo.GetValue(obj);
                if (newValue == null) continue;
                prop.CurrentValue = newValue;

            }
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id)
        {
            var brand = await _dbSet.FindAsync(id);
            if (brand == null) return false;
            _dbSet.Remove(brand);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<Model>> GetModelsForBrand(string brandName)
        {
            var brand = await _context.Set<Brand>()
                          .Include(b => b.Models)
                          .FirstAsync(b => b.BrandName == brandName);
            return brand.Models;
        }







    }
}