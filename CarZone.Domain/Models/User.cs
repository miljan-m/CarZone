using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarZone.Domain.Models
{
    public class User
    {
        [Key]
        [JsonIgnore]
        public int UserId{get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public string Email{get;set;}
        public string Phone{get;set;}
        public string Address{get;set;}
        public string Password{get;set;}

        public List<Listing> PostedListings{get;set;}=[];
        public List<Listing> BoughtListing{get;set;}=[];
        public User()
        {
        }

        public User(int UserId,string FirstName,string LastName,string Email,string Phone, string Address,string Password)
        {
            this.UserId=UserId;
            this.FirstName=FirstName;
            this.LastName=LastName;
            this.Email=Email;
            this.Phone=Phone;
            this.Address=Address;
            this.Password=Password;
        }
    }
}