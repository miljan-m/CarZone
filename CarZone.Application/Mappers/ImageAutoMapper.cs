using AutoMapper;
using CarZone.Application.DTOs.ImageDTOs;
using CarZone.Domain.Models;

namespace CarZone.Application.Mappers
{
    public class ImageAutoMapper : Profile
    {
        public ImageAutoMapper()
        {
            CreateMap<Image, GetImageDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageId))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        }
    }
}