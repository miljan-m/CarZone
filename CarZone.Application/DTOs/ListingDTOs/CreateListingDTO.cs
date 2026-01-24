using CarZone.Domain.Enums;

namespace CarZone.Application.DTOs.ListingDTOs
{
    public class CreateListingDTO
    {

        public int ProductionYear { get; set; }
        public double Price { get; set; }
        public double Mileage { get; set; }
        public double FuelConsuption { get; set; }
        public string AdditionalDescription { get; set; }
        public int ModelId { get; set; }
    }
}