using AutoMapper;
using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Domain.Models;

namespace CarZone.Application.Mappers
{
    public class ModelAutoMapper : Profile
    {
        public ModelAutoMapper()
        {
            CreateMap<Model, GetModelDTO>()
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.ModelName))
                .ForMember(dest=>dest.BrandName,opt=>opt.MapFrom(src=>src.Brand.BrandName));

            CreateMap<UpdateModelDTO,Model>()
                .ForMember(dest=>dest.ModelName,opt=>opt.MapFrom(src=>src.ModelName));
            
            CreateMap<CreateModelDTO,Model>()
                .ForMember(dest=>dest.ModelName,opt=>opt.MapFrom(src=>src.ModelName));

        }
    }
}