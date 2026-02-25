

using CarZone.Application.Interfaces.ServiceInterfaces.ISecurity;
using Microsoft.AspNetCore.Identity;

namespace CarZone.Application.Services.Security
{
    public class PasswordHash : IPasswordHash
    {

        private readonly PasswordHasher<object> _passwordHasher;
        public PasswordHash()
        {
            _passwordHasher = new PasswordHasher<object>();
        }

        public string HashPassword(object user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(object user, string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return result;
        }

        public bool VerifyPassword(object user, string hashedPassword, string providedPassword)
        {
            var result = VerifyHashedPassword(user, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}