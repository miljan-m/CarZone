using AutoMapper;
using CarZone.Application.DTOs.ListingDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Domain.Enums;
using CarZone.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("listings")]
    public class ListingController : ControllerBase
    {
        protected readonly IListingService _service;
        public ListingController(IListingService service)
        {
            _service = service;
        }
        [HttpGet("{listingId}")]
        public async Task<ActionResult<GetListingDTO>> GetListing([FromRoute] int listingId)
        {
            var listing = await _service.GetListingById(listingId);
            if (listing == null) return NotFound();
            return Ok(listing);
        }
        [HttpGet]
        public async Task<ActionResult<GetListingDTO>> GetAllListings()
        {
            var listing = await _service.GetAllListings();
            if (listing == null) return NotFound();
            return Ok(listing);
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateListing([FromRoute] int userId, [FromBody] CreateListingDTO listingDTO, [FromQuery] ListingStatus listingStatus,
                                                       [FromQuery] Transmission transmission, [FromQuery] BodyType bodyType, [FromQuery] EngineType engineType)
        {
            await _service.CreateListing(userId, listingDTO, listingStatus, transmission, bodyType, engineType);
            return Ok();
        }


        [HttpDelete("{listingId}")]
        public async Task<IActionResult> DeleteListing([FromRoute] int listingId)
        {
            await _service.DeleteListing(listingId);
            return Ok();
        }


        [HttpPatch("{listingId}")]
        public async Task<IActionResult> UpdateListing([FromRoute] int listingId, [FromBody] CreateListingDTO listingDTO, [FromQuery] ListingStatus listingStatus,
                                                       [FromQuery] Transmission transmission, [FromQuery] BodyType bodyType, [FromQuery] EngineType engineType)
        {
            await _service.UpdateListing(listingId, listingDTO, listingStatus, transmission, bodyType, engineType);
            return Ok();
        }
    }
}