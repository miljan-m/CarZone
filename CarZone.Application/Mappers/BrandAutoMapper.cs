using System.Runtime.InteropServices;
using AutoMapper;
using CarZone.Application.DTOs.BrandDTOs;
using CarZone.Domain.Models;

namespace CarZone.Application.Mappers
{
    public class BrandAutoMapper : Profile
    {
        public BrandAutoMapper()
        {
            CreateMap<Brand, GetBrandDTO>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName));

            CreateMap<Brand, CreateBrandDTO>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName));

            CreateMap<UpdateBrandDTO, Brand>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName));

            CreateMap<CreateBrandDTO, Brand>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName));

            CreateMap<UpdateBrandDTO, Brand>()
                .ForMember(dest => dest.BrandName, opt => { opt.PreCondition(src => src.BrandName != "string"); opt.MapFrom(src => src.BrandName); });
        }
    }
}