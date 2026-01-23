using AutoMapper;
using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("models")]
    public class ModelController : ControllerBase
    {
        protected readonly IGenericRepository<Model> _repository;
        protected readonly IMapper _mapper;
        public ModelController(IGenericRepository<Model> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper=mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetModelDTO>> GetModel([FromRoute]int id)
        {
            var model=await _repository.GetById(id);
            if(model==null) return NotFound();
            return Ok(_mapper.Map<GetModelDTO>(model));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model>>> GetAllModels()
        {
            var models=await _repository.GetAll();
            var modelsDTO=models.Select(m=>_mapper.Map<GetModelDTO>(m));
            return Ok(modelsDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel([FromRoute] int id)
        {
            var model=await _repository.GetById(id);
            if(model==null) return NotFound();
            await _repository.Delete(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateModel([FromRoute] int id, [FromBody] UpdateModelDTO modelDTO)
        {
            var model=_mapper.Map<Model>(modelDTO);
            await _repository.Update(id,model);
            return Ok();
        }

        [HttpPost("{brandId}")]
        public async Task<IActionResult> CreateModel([FromRoute] int brandId,[FromBody] CreateModelDTO modelDTO)
        {
            await _repository.Create(_mapper.Map<Model>(modelDTO),brandId);
            return Ok();
        }

    }
}