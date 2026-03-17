using AutoMapper;
using CarZone.Application.DTOs.MessageDTOs;
using CarZone.Domain.Models;

namespace CarZone.Application.Mappers
{
    public class MessageAutoMapper : Profile
    {
        public MessageAutoMapper()
        {
            CreateMap<Message, GetMessageDTO>()
                .ForMember(dest=>dest.MessageText,opt=>opt.MapFrom(src=>src.MessageText))
                .ForMember(dest=>dest.ReceiverId,opt=>opt.MapFrom(src=>src.ReceiverId))
                .ForMember(dest=>dest.SenderId,opt=>opt.MapFrom(src=>src.SenderId))
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Sender))
                .ForMember(dest => dest.Receiver, opt => opt.MapFrom(src => src.Receiver))
                .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.SentAt));


            CreateMap<SaveMessageDTO, Message>()
                        .ForMember(dest => dest.ListingId, opt => opt.MapFrom(src => src.ListingId))
                        .ForMember(dest => dest.MessageText, opt => opt.MapFrom(src => src.MessageText))
                        .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.ReceiverId))
                        .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId));


            CreateMap<Message, SaveMessageDTO>();
        }
    }
}