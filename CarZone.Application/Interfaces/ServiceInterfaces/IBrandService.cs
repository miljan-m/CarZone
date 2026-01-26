using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Domain.Models;

namespace CarZone.Application.Interfaces.ServiceInterfaces
{
    public interface IBrandService
    {
        public Task<GetBrandDTO> GetBrandById(int brandId);
        public Task<IEnumerable<GetBrandDTO>> GetAllBrands();
        public Task<bool> DeleteBrand(int id);
        public Task<GetBrandDTO> CreateBrand(CreateBrandDTO brand);
        public Task<GetBrandDTO> UpdateBrand(int id, UpdateBrandDTO brand);
        public Task<IEnumerable<GetModelDTO>> GetModelsForBrand(int brandId);

    }
}