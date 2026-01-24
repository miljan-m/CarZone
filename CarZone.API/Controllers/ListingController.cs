using AutoMapper;
using CarZone.Application.DTOs.ListingDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Domain.Enums;
using CarZone.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("listings")]
    public class ListingController : ControllerBase
    {
        protected readonly IGenericRepository<Listing> _repository;
        protected readonly IMapper _mapper;
        public ListingController(IGenericRepository<Listing> repository,IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }


        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateListing([FromRoute]int userId,[FromBody] CreateListingDTO listingDTO,[FromQuery] ListingStatus listingStatus,
                                                       [FromQuery] Transmission transmission,[FromQuery] BodyType bodyType,[FromQuery] EngineType engineType)
        {
            var listing=_mapper.Map<Listing>(listingDTO);
                listing.ListingStatus=listingStatus;
                listing.Transmission=transmission;
                listing.BodyType=bodyType;
                listing.EngineType=engineType;
            await _repository.Create(listing,userId);
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetListingDTO>> GetListing([FromRoute]int id)
        {
            var listing=await _repository.GetById(id);
            if(listing==null) return NotFound();
            return Ok(_mapper.Map<GetListingDTO>(listing));
        }

         [HttpGet]
        public async Task<ActionResult<GetListingDTO>> GetAllListings()
        {
            var listing=await _repository.GetAll();
            if(listing==null) return NotFound();
            var listingDTOs=listing.Select(l=>_mapper.Map<GetListingDTO>(l));
            return Ok(listingDTOs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing([FromRoute]int id)
        {
            await _repository.Delete(id);
            return Ok();
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateListing([FromRoute]int id,[FromBody] CreateListingDTO listingDTO,[FromQuery] ListingStatus listingStatus,
                                                       [FromQuery] Transmission transmission,[FromQuery] BodyType bodyType,[FromQuery] EngineType engineType)
        {
            var listing=await _repository.GetById(id);
            listing.AdditionalDescription=listingDTO.AdditionalDescription;
            listing.BodyType=bodyType;
            listing.EngineType=engineType;
            listing.FuelConsuption=listingDTO.FuelConsuption;
            listing.ListingStatus=listingStatus;
            listing.Mileage=listingDTO.Mileage;
            listing.Transmission=transmission;
            listing.Price=listingDTO.Price;
            listing.ProductionYear=listingDTO.ProductionYear;
            listing.ModelId=listingDTO.ModelId;


            await _repository.Update(id,listing);
            return Ok();
        }
    }
}