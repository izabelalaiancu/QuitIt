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
            //CreateMap<Vice, ViceDto>().ReverseMap();
        }
    }
}
