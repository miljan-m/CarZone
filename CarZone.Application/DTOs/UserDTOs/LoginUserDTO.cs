using System.Dynamic;
using System.Text.Json.Serialization;

namespace CarZone.Application.DTOs.UserDTOs
{
    public class LoginUserDTO
    {


        public string Email { get; set; }
        public string Password { get; set; }
    }
}