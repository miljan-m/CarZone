using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarZone.Application.Interfaces;
using CarZone.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarZone.Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IOptions<JwtOptions> _options;
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options;
        }

        public string Generate(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email,user.Email),
                new(ClaimTypes.NameIdentifier,user.UserId.ToString())

            };

                
            foreach(string role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_options.Value.Issuer, _options.Value.Audience, claims, null, DateTime.UtcNow.AddHours(1), credentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}