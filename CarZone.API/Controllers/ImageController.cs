using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Application.Services;
using CarZone.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {

        private IImageService _service;
        public ImageController(IImageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetAllImages()
        {
            var images = await _service.GetAllImages();
            return Ok(images);
        }



        [HttpGet("{listingId}")]
        public async Task<ActionResult<Image>> GetImagesByListingId([FromRoute] int listingId)
        {

            var images = await _service.GetImageByListingId(listingId);
            return Ok(images);
        }

    }
}