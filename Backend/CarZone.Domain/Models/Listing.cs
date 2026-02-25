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

        public ListingStatus ListingStatus { get; set; } = ListingStatus.Active;

        public List<Image> Images { get; set; }

        public Listing()
        {
        }

        public Listing(int userId, int modelId, int productionYear, EngineType engineType, BodyType bodyType,
        Transmission transmission, double price, double mileage, double fuelConsumption, string additionalDescription)
        {
            if (userId <= 0)
                throw new ArgumentException("UserId must be a positive number.");

            if (modelId <= 0)
                throw new ArgumentException("ModelId must be a positive number.");

            if (productionYear < 1900 || productionYear > DateTime.UtcNow.Year + 1)
                throw new ArgumentException("Invalid production year.");

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (mileage < 0)
                throw new ArgumentException("Mileage cannot be negative.");

            if (fuelConsumption <= 0)
                throw new ArgumentException("Fuel consumption must be greater than zero.");

            UserId = userId;
            ModelId = modelId;
            ProductionYear = productionYear;
            EngineType = engineType;
            BodyType = bodyType;
            Transmission = transmission;
            Price = price;
            Mileage = mileage;
            FuelConsuption = fuelConsumption;
            AdditionalDescription = additionalDescription;
            PublishedDate = DateOnly.FromDateTime(DateTime.UtcNow);
            ListingStatus = ListingStatus.Active;
        }

    }
}