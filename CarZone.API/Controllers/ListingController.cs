using CarZone.Application.DTOs.ListingDTOs;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("listings")]
    public class ListingController : ControllerBase
    {
        protected readonly IListingService _service;
        private readonly IWebHostEnvironment _env;

        public ListingController(IListingService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
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
        public async Task<IActionResult> CreateListing([FromRoute] int userId, [FromForm] CreateListingDTO listingDTO)
        {
            var imageURL = new List<string>();
            if (listingDTO.Images != null && listingDTO.Images.Any())
            {
                var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var uploadPath = Path.Combine(webRoot, "images");
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                foreach (var image in listingDTO.Images)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    var filePath = Path.Combine(uploadPath, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);
                    imageURL.Add($"images/{fileName}");
                }
            }

            var result = await _service.CreateListing(userId, listingDTO, imageURL);

            return Ok(result);
        }


        [HttpDelete("{listingId}")]
        public async Task<IActionResult> DeleteListing([FromRoute] int listingId)
        {
            await _service.DeleteListing(listingId);
            return Ok();
        }


        [HttpPatch("{listingId}")]
        public async Task<IActionResult> UpdateListing([FromRoute] int listingId, [FromBody] UpdateListingDTO listingDTO, [FromQuery] ListingStatus listingStatus,
                                                       [FromQuery] Transmission transmission, [FromQuery] BodyType bodyType, [FromQuery] EngineType engineType)
        {
            await _service.UpdateListing(listingId, listingDTO, listingStatus, transmission, bodyType, engineType);
            return Ok();
        }
    }
}