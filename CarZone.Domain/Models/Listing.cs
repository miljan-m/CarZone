using System.ComponentModel.DataAnnotations;
using CarZone.Domain.Enums;

namespace CarZone.Domain.Models
{
    public class Listing
    {

        [Key]
        public int ListingID { get; set; }

        //user navigation
        public int UserId { get; set; }
        public User User { get; set; }

        public int? BuyerId { get; set; }
        public User Buyer { get; set; }

        //model navigation
        public int ModelId { get; set; }
        public Model Model { get; set; }

        //vehicle informations
        public int ProductionYear { get; set; }
        public EngineType EngineType { get; set; }
        public BodyType BodyType { get; set; }
        public double Price { get; set; }
        public Transmission Transmission { get; set; }
        public double Mileage { get; set; }
        public double FuelConsuption { get; set; }
        //listing info
        public DateOnly PublishedDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public string AdditionalDescription { get; set; }

        public ListingStatus ListingStatus { get; set; }

        public Listing()
        {
        }

    }
}