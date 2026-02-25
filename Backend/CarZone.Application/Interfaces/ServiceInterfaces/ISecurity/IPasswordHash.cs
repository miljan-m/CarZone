using Microsoft.AspNetCore.Identity;

namespace CarZone.Application.Interfaces.ServiceInterfaces.ISecurity
{
    public interface IPasswordHash : IPasswordHasher<object>
    {
        public bool VerifyPassword(object user, string hashedPassword, string providedPassword);
    }
}