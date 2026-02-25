using CarZone.Domain.Models;

namespace CarZone.Application.Interfaces
{
    public interface IJwtProvider
    {
        public string Generate(User user);
    }
}