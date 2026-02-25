using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Application.DTOs.ModelDTOs;

namespace CarZone.Application.Interfaces.ServiceInterfaces
{
    public interface IModelService
    {
        public Task<GetModelDTO> GetModelById(int modelId);
        public Task<IEnumerable<GetModelDTO>> GetAllModels();
        public Task<bool> DeleteModel(int modelId);
        public Task<GetModelDTO> CreateModel(CreateModelDTO model,int brandId); 
        public Task<GetModelDTO> UpdateModel(int id,UpdateModelDTO model);

        public Task<GetBrandDTO> GetBrandForModel(int modelId);
    }
}