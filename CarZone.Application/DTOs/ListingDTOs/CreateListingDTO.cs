using CarZone.Domain.Enums;
using Microsoft.AspNetCore.Http;

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

        public Transmission Transmission { get; set; }
        public BodyType BodyType { get; set; }
        public EngineType EngineType { get; set; }
        // slike
        public List<IFormFile> Images { get; set; }
    }
}