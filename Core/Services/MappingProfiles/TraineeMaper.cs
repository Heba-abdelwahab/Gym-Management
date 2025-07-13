using AutoMapper;
using Domain.Entities;
using Shared;
using Shared.TraineeGym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    internal class TraineeMapper:Profile
    {
        public TraineeMapper()
        {
            CreateMap<Trainee, TraineeToReturnDto>()


            .ForMember(dest => dest.Name, opt => 
            opt.MapFrom(src => $"{src.AppUser.FirstName} {src.AppUser.LastName}"))
.ForMember(dest => dest.MemberShipName, opt => opt.MapFrom(src => src.Membership.Name))
            .ForMember(dest => dest.CoachName, opt => opt.MapFrom(src => $"{src.Coach!.AppUser.FirstName} {src.Coach.AppUser.LastName}"));


            CreateMap<Trainee, TraineeSubscriptionsToReturnDto>();

            CreateMap<Trainee, TraineeDataToReturnDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight));

        }
    }
}
