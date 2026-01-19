using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarZone.Domain.Models
{
    public class User
    {
        [Key]
        [JsonIgnore]
        public int UserId{get;set;}
        public string Name{get;set;}
        public string LastName{get;set;}
        public string Email{get;set;}
        public string Phone{get;set;}
        public string Address{get;set;}

        public User()
        {
        }

        public User(int UserId,string Name,string LastName,string Email,string Phone, string Address)
        {
            this.UserId=UserId;
            this.Name=Name;
            this.LastName=LastName;
            this.Email=Email;
            this.Phone=Phone;
            this.Address=Address;
        }
    }
}