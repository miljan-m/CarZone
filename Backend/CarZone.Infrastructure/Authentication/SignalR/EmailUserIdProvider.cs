using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace CarZone.Infrastructure.Authentication.SignalR
{
    public class EmailUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(JwtRegisteredClaimNames.Email)?.Value
           ?? connection.User?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}