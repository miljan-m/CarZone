using CarZone.Application.DTOs.ImageDTOs;
using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Application.DTOs.UserDTOs;
using CarZone.Domain.Enums;
using CarZone.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace CarZone.Application.DTOs.ListingDTOs
{
    public class GetListingDTO
    {
        public int ListingId { get; set; }
        //user navigation
        public GetUserDTO User { get; set; }
        //model navigation
        public GetModelDTO Model { get; set; }
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
        public List<GetImageDTO> Images { get; set; }


    }
}