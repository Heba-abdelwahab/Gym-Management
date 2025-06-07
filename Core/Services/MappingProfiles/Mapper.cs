using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
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

            CreateMap<MealDto, Meal>().ReverseMap();

            CreateMap<MealScheduleDto, MealSchedule>()
                .ForMember(dest => dest.schedule,
                           opt => opt.MapFrom(src => new Schedule { StartDay = src.StartDate, EndDay = src.EndDate }));
        }
    }
}
