using AutoMapper;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public sealed class GymOwnerProfile:Profile
    {
        public GymOwnerProfile()
        {
            CreateMap<GymOwner, GymOwnerToReturnDto>()
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber));
        }
    }
}
