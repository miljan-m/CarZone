using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Domain.Models;
using CarZone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CarZone.Infrastructure.Repositories
{
    public class ModelRepository : IModelRepository
    {
        protected readonly CarZoneDBContext _context;
        protected readonly DbSet<Model> _dbset;

        protected readonly CarZoneDBContext _contextBrand;
        protected readonly DbSet<Brand> _dbsetBrand;
        public ModelRepository(CarZoneDBContext context, CarZoneDBContext contextBrand)
        {
            _context = context;
            _dbset = context.Set<Model>();
            _contextBrand = contextBrand;
            _dbsetBrand = _contextBrand.Set<Brand>();
        }
        public async Task<Model> GetById(int modelId)
        {
            var model = await _dbset.Include(m=>m.Brand).FirstAsync(m=>m.ModelId==modelId);
            if (model == null) return null;
            return model;
        }

        public async Task<IEnumerable<Model>> GetAll()
        {
            var models = await _dbset.Include(m=>m.Brand).ToListAsync();
            if (models.Any()) return models;
            return null;
        }
        public async Task<Model> Create(Model obj, int id = int.MinValue)
        {
            var brand = await _dbsetBrand.FindAsync(id);
            if (brand == null) return null;
            obj.Brand = brand;
            obj.BrandId = brand.BrandId;
            await _dbset.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> Delete(int modelId)
        {
            var model = await _dbset.FindAsync(modelId);
            if (model == null) return false;
            _dbset.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int modelId, Model obj)
        {
            var model = await _dbset.FindAsync(modelId);
            if (model == null) return false;
            var properties = _context.Entry(model).Properties;

            foreach (var prop in properties)
            {
                if (prop.Metadata.IsPrimaryKey()) continue;
                if (prop.Metadata.IsForeignKey()) continue;

                var propInfo=typeof(Model).GetType().GetProperty(prop.Metadata.Name);
                if(propInfo==null) return false;
                var newValue=propInfo.GetValue(obj);
                if(newValue==null) return false;
                prop.CurrentValue=newValue;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Brand> GetBrandForModel(int modelId)
        {
            var model=await _dbset.Include(m=>m.Brand).FirstAsync(m=>m.ModelId==modelId);
            if(model==null) return null;
            return model.Brand;
        }
    }
}