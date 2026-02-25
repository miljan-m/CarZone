using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Domain.Models;

namespace CarZone.Application.Interfaces.Repositories
{
    public interface IModelRepository : IGenericRepository<Model>
    {
        public Task<Brand> GetBrandForModel(int modelId);
    }
}