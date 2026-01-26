using AutoMapper;
using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("models")]
    public class ModelController : ControllerBase
    {
        protected readonly IModelService _service;
        protected readonly IMapper _mapper;
        public ModelController(IModelService service)
        {
            _service = service;
        }

        [HttpGet("{modelId}")]
        public async Task<ActionResult<GetModelDTO>> GetModel([FromRoute]int modelId)
        {
            var model=await _service.GetModelById(modelId);
            if(model==null) return NotFound();
            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model>>> GetAllModels()
        {
            var models=await _service.GetAllModels();
            return Ok(models);
        }

        [HttpDelete("{modelId}")]
        public async Task<IActionResult> DeleteModel([FromRoute] int modelId)
        {
            
            var isDeleted=await _service.DeleteModel(modelId);
            if(isDeleted) return Ok();
            return NotFound();
        }

        [HttpPatch("{modelId}")]
        public async Task<IActionResult> UpdateModel([FromRoute] int modelId, [FromBody] UpdateModelDTO modelDTO)
        {
            var updatedModel=await _service.UpdateModel(modelId,modelDTO);
            if(updatedModel==null) NotFound();
            return Ok();
        }

        [HttpPost("{brandId}")]
        public async Task<IActionResult> CreateModel([FromRoute] int brandId,[FromBody] CreateModelDTO modelDTO)
        {
            await _service.CreateModel(modelDTO,brandId);
            return Ok();
        }

        [HttpGet("{modelId}/brand")]
        public async Task<ActionResult<GetBrandDTO>> GetBrandForModel([FromRoute]int modelId)
        {
            var brand=await _service.GetBrandForModel(modelId);
            return Ok(brand);
        }

    }
}