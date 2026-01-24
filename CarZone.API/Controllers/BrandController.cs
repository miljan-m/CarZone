using AutoMapper;
using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("brands")]
    public class BrandController : ControllerBase
    {
        protected readonly IGenericRepository<Brand> _repository;
        protected readonly IMapper _mapper;
        public BrandController(IGenericRepository<Brand> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper=mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBrandDTO>> GetBrand([FromRoute] int id)
        {
            var brand = await _repository.GetById(id);
            if (brand == null) return NotFound();
            return Ok(_mapper.Map<GetBrandDTO>(brand));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBrandDTO>>> GetAllBrands()
        {
            var brands = await _repository.GetAll();
            if (brands.Count() == 0) return NotFound();
            var brandDTOs= brands.Select(b=>_mapper.Map<GetBrandDTO>(b));
            return Ok(brandDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDTO brandDTO)
        {
            await _repository.Create(_mapper.Map<Brand>(brandDTO));
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBrand([FromRoute] int id, [FromBody] UpdateBrandDTO brandDTO)
        {
            await _repository.Update(id, _mapper.Map<Brand>(brandDTO));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand([FromRoute] int id)
        {
            await _repository.Delete(id);
            return Ok();
        }

        [HttpGet("{brandId}/models")]
        public async Task<ActionResult<IEnumerable<GetModelDTO>>> GetModelsForBrand([FromRoute]int brandId)
        {
            var brand=await _repository.GetById(brandId);
            var models=brand.Models;
            return Ok(models.Select(m=>_mapper.Map<GetModelDTO>(m)));
        }

    }
}