using AutoMapper;
using CarZone.Application.DTOs.ListingDTOs;
using CarZone.Domain.Models;

namespace CarZone.Application.Mappers
{
    public class ListingAutoMapper : Profile
    {
        public ListingAutoMapper()
        {
            CreateMap<CreateListingDTO, Listing>()
                    .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => src.ProductionYear))
                    .ForMember(dest => dest.FuelConsuption, opt => opt.MapFrom(src => src.FuelConsuption))
                    .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                    .ForMember(dest => dest.ModelId, opt => opt.MapFrom(src => src.ModelId));

            CreateMap<UpdateListingDTO, Listing>()
                    .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => src.ProductionYear))
                    .ForMember(dest => dest.FuelConsuption, opt => opt.MapFrom(src => src.FuelConsuption))
                    .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                    .ForMember(dest => dest.ModelId, opt => opt.MapFrom(src => src.ModelId));


            CreateMap<Listing, GetListingDTO>()
                    .ForMember(dest => dest.AdditionalDescription, opt => opt.MapFrom(src => src.AdditionalDescription))
                    .ForMember(dest => dest.BodyType, opt => opt.MapFrom(src => src.BodyType))
                    .ForMember(dest => dest.EngineType, opt => opt.MapFrom(src => src.EngineType))
                    .ForMember(dest => dest.ListingStatus, opt => opt.MapFrom(src => src.ListingStatus))
                    .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                    .ForMember(dest => dest.FuelConsuption, opt => opt.MapFrom(src => src.FuelConsuption))
                    .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => src.ProductionYear))
                    .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate))
                    .ForMember(dest => dest.Transmission, opt => opt.MapFrom(src => src.Transmission))
                    .ForMember(dest=>dest.User,opt=>opt.MapFrom(src=>src.User))
                    .ForMember(dest=>dest.Model,opt=>opt.MapFrom(src=>src.Model));
            ;




        }
    }
}