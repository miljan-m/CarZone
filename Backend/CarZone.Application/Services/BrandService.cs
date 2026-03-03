using AutoMapper;
using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Application.Exceptions.BrandExceptions;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Application.Validation.CreateValidation;
using CarZone.Application.Validation.UpdateValidation;
using CarZone.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace CarZone.Application.Services
{
    public class BrandService : IBrandService
    {
        protected readonly IBrandRepository _repository;
        protected readonly IMapper _mapper;


        public BrandService(IBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<GetBrandDTO> CreateBrand(CreateBrandDTO brandDTO)
        {
            var validator = new CreateBrandDTOValidator();
            var result = validator.Validate(brandDTO);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
                throw new BrandValidationException("Validation exception - Brand data is invalid", StatusCodes.Status422UnprocessableEntity);
            }
            var brand = await _repository.Create(_mapper.Map<Brand>(brandDTO)) ?? throw new BrandAlreadyExistException(brandDTO.BrandName, StatusCodes.Status409Conflict);
            return _mapper.Map<GetBrandDTO>(brand);

        }

        public async Task<bool> DeleteBrand(int brandId)
        {
            if (!await _repository.Delete(brandId)) throw new BrandNotFoundException(brandId.ToString(), StatusCodes.Status404NotFound);
            return true;
        }

        public async Task<IEnumerable<GetBrandDTO>> GetAllBrands()
        {
            var brands = await _repository.GetAll();
            return brands.Select(b => _mapper.Map<GetBrandDTO>(b));
        }

        public async Task<GetBrandDTO> GetBrandById(int brandId)
        {
            var brand = await _repository.GetById(brandId) ?? throw new BrandNotFoundException(brandId.ToString(), StatusCodes.Status404NotFound);
            return _mapper.Map<GetBrandDTO>(brand);

        }

        public async Task<IEnumerable<GetModelDTO>> GetModelsForBrand(string brandName)
        {
            var models = await _repository.GetModelsForBrand(brandName) ?? throw new BrandNotFoundException(brandName, StatusCodes.Status404NotFound);
            return models.Select(m => _mapper.Map<GetModelDTO>(m));
        }

        public async Task<GetBrandDTO> UpdateBrand(int brandId, UpdateBrandDTO brandDTO)
        {

            var validator = new UpdateBrandDTOValidator();
            var result = validator.Validate(brandDTO);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Validation eerror: {error.ErrorMessage}");
                }
                throw new BrandValidationException("Validation exception - Brand data is invalid", StatusCodes.Status422UnprocessableEntity);

            }
            if (await _repository.Update(brandId, _mapper.Map<Brand>(brandDTO))) throw new BrandNotFoundException(brandId.ToString(), StatusCodes.Status404NotFound);
            var brand = await _repository.GetById(brandId);
            return _mapper.Map<GetBrandDTO>(brand);


        }

    }
}