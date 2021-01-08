using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models.Entities;
using Services.Dtos;
using WebApi.DTOs;

namespace WebApi.Helpers
{
    public class CustomMapperProfile : Profile
    {
        public CustomMapperProfile()
        {
            CreateMap<UserDto, User>().ForMember(d => d.UserName, s => s.MapFrom(x => x.Email));
            CreateMap<User, UserDto>();
            CreateMap<Vice, ViceDto>().ForMember(d => d.ViceId, s => s.MapFrom(x => x.Id)).ReverseMap();
            CreateMap<UserVice, Vice>().ForMember(d => d.Name, s => s.MapFrom(x => x.Vice.Name))
                .ForMember(d => d.Id, s => s.MapFrom(x => x.ViceId))
                .ReverseMap();

        }
    }
}
