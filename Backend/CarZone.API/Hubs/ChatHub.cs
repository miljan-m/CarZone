using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CarZone.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {

        public async Task SendMessage(string receiverEmail, string message)
        {
            var senderEmail = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == JwtRegisteredClaimNames.Email)?.Value;

            await Clients.User(receiverEmail).SendAsync("ReceiveMessage", senderEmail, message);
            Console.WriteLine($"Sender: {senderEmail}, Receiver: {receiverEmail}");
        }

    }
}