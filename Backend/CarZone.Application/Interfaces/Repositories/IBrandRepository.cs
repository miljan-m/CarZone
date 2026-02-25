using CarZone.Domain.Models;

namespace CarZone.Application.Interfaces.Repositories
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        public Task<IEnumerable<Model>> GetModelsForBrand(string brandName);
    }
}