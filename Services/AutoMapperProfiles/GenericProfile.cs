using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DataLayer.Models.Entities;
using Services.Dtos;

namespace Services.AutoMapperProfiles
{
    public class GenericProfile : Profile
    {
        public GenericProfile()
        {
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<UserVice, ViceDto>()
                .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Vice.Name))
                .ForMember(src => src.ViceId, opt => opt.MapFrom(dest => dest.Vice.Id))
                .ForMember(src => src.Score, opt => opt.MapFrom(dest => dest.Score))
                .ReverseMap();
        }
    }
}
