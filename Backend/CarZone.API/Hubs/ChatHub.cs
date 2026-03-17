using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CarZone.Application.DTOs.MessageDTOs;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CarZone.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _service;
        private readonly IUserService _userService;
        private readonly IListingService _listingService;
        public ChatHub(IMessageService service, IUserService userService, IListingService listingService)
        {
            _service = service;
            _userService = userService;
            _listingService = listingService;
        }

        public async Task SendMessage(string receiverEmail, SaveMessageDTO message)
        {

            
            var senderEmail = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            await Clients.User(receiverEmail).SendAsync("ReceiveMessage", message);
            await _service.SaveMessage(message);
        }

    }
}