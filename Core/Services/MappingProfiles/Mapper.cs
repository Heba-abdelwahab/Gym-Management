using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Shared;

namespace Services.MappingProfiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            // WorkDay mappings
            CreateMap<WorkDayDto, WorkDay>().ReverseMap();

            // Coach mappings
            CreateMap<Coach, CoachToReturnDto>()
                .ForCtorParam("FirstName", opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForCtorParam("LastName", opt => opt.MapFrom(src => src.AppUser.LastName))
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id)) // You were missing this
                .ForCtorParam("CurrentCapcity", opt => opt.MapFrom((src, ctx) =>
                    src.GymCoaches?.FirstOrDefault(g => g.GymId == (int)ctx.Items["gymid"])?.CurrentCapcity ?? 0))
                .ForCtorParam("Specializations", opt => opt.MapFrom(src =>
                    src.Specializations.ToString()));

            // Meal mappings
            CreateMap<MealDto, Meal>().ReverseMap();

            // MealSchedule mappings
            CreateMap<MealScheduleDto, MealSchedule>()
                .ForMember(dest => dest.schedule,
                    opt => opt.MapFrom(src => new Schedule
                    {
                        StartDay = src.StartDate,
                        EndDay = src.EndDate
                    }));

            // Address & Location mappings
            CreateMap<AddressDto, Address>();
            CreateMap<LocationDto, Location>();
        }
    }
}