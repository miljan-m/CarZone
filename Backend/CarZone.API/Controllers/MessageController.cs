using CarZone.Application.DTOs.MessageDTOs;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("message")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;
        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<GetMessageDTO>> GetAllMessages()
        {
            return await _service.GetAllMessages();
        }

    }
}