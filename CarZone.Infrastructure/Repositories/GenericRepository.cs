using CarZone.Application.Interfaces.Repositories;
using CarZone.Domain.Models;
using CarZone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CarZone.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CarZoneDBContext _context;
        public GenericRepository(CarZoneDBContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T obj,int id=int.MinValue)
        {
            if (typeof(T) == typeof(Model))
            {
                var brand=await _context.Set<Brand>().FindAsync(id);
                if(brand==null) return null;
                var model = obj as Model;
                model.BrandId=id;
                await _context.Set<Model>().AddAsync(model);
                await _context.SaveChangesAsync();
                return model as T;
            }


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
            if (typeof(T) == typeof(Model))
            {
               var models=await _context.Set<Model>().Include(m=>m.Brand).ToListAsync();
               var mod=models.Select(m=>(T)(object)m);
               return mod;
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            if (typeof(T) == typeof(Model))
            {
                var modelObj = await _context.Set<Model>()
                                      .Include(m => m.Brand)
                                      .FirstOrDefaultAsync(m => m.ModelId == id);
                                      
                if (modelObj == null) return default;
                return (T)(object)modelObj;
            }

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

                if (property.Metadata.IsForeignKey())
                    continue;

                var propInfo = typeof(T).GetProperty(property.Metadata.Name);
                if (propInfo == null)
                    continue; // polje ne postoji u DTO

                var newValue = propInfo.GetValue(obj);
                if (newValue == null)
                    continue; // preskoči null vrednosti (obavezna polja ostaju netaknuta)

                property.CurrentValue = newValue;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}