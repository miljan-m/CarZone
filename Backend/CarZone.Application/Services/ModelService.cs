using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Application.Exceptions.BrandExceptions;
using CarZone.Application.Exceptions.ModelExceptions;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Application.Validation.CreateValidation;
using CarZone.Application.Validation.UpdateValidation;
using CarZone.Domain.Models;
using Microsoft.AspNetCore.Http;

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
            var validator = new CreateModelDTOValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
                throw new ModelValidationException("Validation exception - Model data is invalid", StatusCodes.Status422UnprocessableEntity);
            }
            var x = await _repository.Create(_mapper.Map<Model>(model), brandId) ?? throw new ModelAlreadyExistException(model.ModelName, StatusCodes.Status409Conflict);
            return _mapper.Map<GetModelDTO>(x);
        }

        public async Task<bool> DeleteModel(int modelId)
        {
            if (await _repository.Delete(modelId) == false) throw new ModelNotFoundException(modelId.ToString(), StatusCodes.Status404NotFound);
            return true;
        }

        public async Task<IEnumerable<GetModelDTO>> GetAllModels()
        {
            var models = await _repository.GetAll();
            return models.Select(m => _mapper.Map<GetModelDTO>(m));
        }

        public async Task<GetModelDTO> GetModelById(int modelId)
        {
            var model = await _repository.GetById(modelId) ?? throw new ModelNotFoundException(modelId.ToString(), StatusCodes.Status404NotFound);
            return _mapper.Map<GetModelDTO>(model);
        }

        public async Task<GetModelDTO> UpdateModel(int brandId, UpdateModelDTO modelDTO)
        {

            var validator = new UpdateModelDTOValidator();
            var result = validator.Validate(modelDTO);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Validation eerror: {error.ErrorMessage}");
                }
                throw new ModelValidationException("Validation exception - Model data is invalid", StatusCodes.Status422UnprocessableEntity);
            }
            var model = await _repository.Update(brandId, _mapper.Map<Model>(modelDTO));
            if (!model) throw new BrandNotFoundException(brandId.ToString(), StatusCodes.Status404NotFound);

            return _mapper.Map<GetModelDTO>(model);

        }

        public async Task<GetBrandDTO> GetBrandForModel(int modelId)
        {
            var brand = await _repository.GetBrandForModel(modelId);
            if (brand == null) throw new ModelNotFoundException(modelId.ToString(), StatusCodes.Status404NotFound);
            return _mapper.Map<GetBrandDTO>(brand);
        }
    }
}