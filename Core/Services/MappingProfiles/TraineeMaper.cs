using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Shared;

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

        }
    }
}
