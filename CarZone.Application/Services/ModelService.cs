using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Application.Validation;
using CarZone.Domain.Models;

namespace CarZone.Application.Services
{
    public class ModelService : IModelService
    {
        protected readonly IModelRepository _repository;
        protected readonly IMapper _mapper;

        public ModelService(IModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetModelDTO> CreateModel(CreateModelDTO model, int brandId)
        {
            var validator=new CreateModelDTOValidator();
            var result=validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
                throw new ValidationException("Model data is invalid");
            }
            var x = await _repository.Create(_mapper.Map<Model>(model), brandId);
            return _mapper.Map<GetModelDTO>(x);
        }

        public Task<bool> DeleteModel(int modelId)
        {
            return _repository.Delete(modelId);
        }

        public async Task<IEnumerable<GetModelDTO>> GetAllModels()
        {
            var models = await _repository.GetAll();
            return models.Select(m => _mapper.Map<GetModelDTO>(m));
        }

        public async Task<GetModelDTO> GetModelById(int modelId)
        {
            var model = await _repository.GetById(modelId);
            if (model == null) return null;
            return _mapper.Map<GetModelDTO>(model);
        }

        public async Task<GetModelDTO> UpdateModel(int brandId, UpdateModelDTO modelDTO)
        {
            var model = await _repository.Update(brandId, _mapper.Map<Model>(modelDTO));
            return _mapper.Map<GetModelDTO>(model);

        }

        public async Task<GetBrandDTO> GetBrandForModel(int modelId)
        {
            var brand=await _repository.GetBrandForModel(modelId);
            if(brand==null) return null;
            return _mapper.Map<GetBrandDTO>(brand);
        }
    }
}