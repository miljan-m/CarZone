using CarZone.Domain.Enums;

namespace CarZone.Application.DTOs.ListingDTOs
{
    public class UpdateListingDTO
    {
        public int ProductionYear { get; set; }
        public double Price { get; set; }
        public double Mileage { get; set; }
        public double FuelConsuption { get; set; }
        public string AdditionalDescription { get; set; }
        public ListingStatus ListingStatus { get; set; }
        public Transmission Transmission { get; set; }
        public BodyType BodyType { get; set; }
        public EngineType EngineType { get; set; }
    }
}