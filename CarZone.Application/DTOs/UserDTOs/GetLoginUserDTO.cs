namespace CarZone.Application.DTOs.UserDTOs
{
    public class GetLoginUserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        public string Address { get; set; }

    }
}