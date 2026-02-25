using AutoMapper;
using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Application.Validation.CreateValidation;
using CarZone.Application.Validation.UpdateValidation;
using CarZone.Domain.Models;
using FluentValidation;

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
                throw new ValidationException("Brand data is invalid");
            }
            var brand = await _repository.Create(_mapper.Map<Brand>(brandDTO));
            return _mapper.Map<GetBrandDTO>(brand);

        }

        public async Task<bool> DeleteBrand(int brandId)
        {
            await _repository.Delete(brandId);
            return true;
        }

        public async Task<IEnumerable<GetBrandDTO>> GetAllBrands()
        {
            var brands = await _repository.GetAll();
            return brands.Select(b => _mapper.Map<GetBrandDTO>(b));
        }

        public async Task<GetBrandDTO> GetBrandById(int brandId)
        {
            var brand = await _repository.GetById(brandId);
            return _mapper.Map<GetBrandDTO>(brand);

        }

        public async Task<IEnumerable<GetModelDTO>> GetModelsForBrand(string brandName)
        {
            var models = await _repository.GetModelsForBrand(brandName);
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
                throw new ValidationException("Brand data is invalid");
            }
            await _repository.Update(brandId, _mapper.Map<Brand>(brandDTO));
            var brand = await _repository.GetById(brandId);
            return _mapper.Map<GetBrandDTO>(brand);


        }

    }
}