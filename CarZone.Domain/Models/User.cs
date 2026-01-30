using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarZone.Domain.Models
{
    public class User
    {
        [Key]
        [JsonIgnore]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public List<Listing> PostedListings { get; set; } = [];
        public List<Listing> BoughtListing { get; set; } = [];
        public User()
        {
        }

        public User(int UserId, string FirstName, string LastName, string Email, string Phone, string Address, string Password)
        {
            if (UserId < 0)
                throw new ArgumentException("UserId cannot be negative.");

            if (string.IsNullOrWhiteSpace(FirstName))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(LastName))
                throw new ArgumentException("Last name is required.");

            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("Email is required.");

            if (string.IsNullOrWhiteSpace(Password))
                throw new ArgumentException("Password is required.");

            if (string.IsNullOrWhiteSpace(Address))
                throw new ArgumentException("Address is required.");
                
            this.UserId = UserId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.Password = Password;
        }
    }
}