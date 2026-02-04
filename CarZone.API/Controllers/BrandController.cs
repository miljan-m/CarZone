using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("brands")]
    public class BrandController : ControllerBase
    {
        protected readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("{brandId}")]
        public async Task<ActionResult<GetBrandDTO>> GetBrand([FromRoute] int brandId)
        {
            var brand = await _brandService.GetBrandById(brandId);
            if (brand == null) return NotFound();
            return Ok(brand);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBrandDTO>>> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrands();
            if (brands.Any()) return Ok(brands);
            return NotFound();

        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<GetBrandDTO>> CreateBrand([FromBody] CreateBrandDTO brandDTO)
        {
            var brand = await _brandService.CreateBrand(brandDTO);
            return Ok(brand);
        }

        [HttpPatch("{brandId}")]
        [Authorize(Roles = Role.Admin)]

        public async Task<IActionResult> UpdateBrand([FromRoute] int brandId, [FromBody] UpdateBrandDTO brandDTO)
        {
            await _brandService.UpdateBrand(brandId, brandDTO);
            return Ok();
        }

        [HttpDelete("{brandId}")]
        [Authorize(Roles = Role.Admin)]

        public async Task<IActionResult> DeleteBrand([FromRoute] int brandId)
        {
            var isDeleted = await _brandService.DeleteBrand(brandId);
            if (isDeleted) return Ok();
            return NotFound();
        }

        [HttpGet("{brandId}/models")]
        public async Task<ActionResult<IEnumerable<GetModelDTO>>> GetModelsForBrand([FromRoute] int brandId)
        {
            var models = await _brandService.GetModelsForBrand(brandId);
            return Ok(models);
        }

    }
}