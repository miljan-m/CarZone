using CarZone.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CarZone.API.Controllers
{
    [ApiController]
    public class EnumController : ControllerBase
    {
        public EnumController()
        {
        }

        [HttpGet("bodyTypes")]
        public IActionResult GetBodyTypes()
        {
            var bodyType = Enum.GetNames(typeof(BodyType));
            return Ok(bodyType);
        }

        [HttpGet("engineTypes")]
        public IActionResult GetEngineType()
        {
            var engineType = Enum.GetNames(typeof(EngineType));
            return Ok(engineType);
        }

        [HttpGet("transmission")]
        public IActionResult GetTransmission()
        {
            var transmission = Enum.GetNames(typeof(Transmission));
            return Ok(transmission);
        }
    }
}