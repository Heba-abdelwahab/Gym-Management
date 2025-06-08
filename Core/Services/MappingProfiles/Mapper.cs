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
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<WorkDayDto, WorkDay>().ReverseMap();
            CreateMap<Coach, CoachToReturnDto>()
                .ForCtorParam("FirstName", x => x.MapFrom(src => src.AppUser.FirstName))
                .ForCtorParam("LastName", x => x.MapFrom(src => src.AppUser.LastName));

        }
    }
}
