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
            CreateMap<WorkDayDto, WorkDay>().ReverseMap();
            CreateMap<Coach, CoachToReturnDto>()
                .ForCtorParam("FirstName", x => x.MapFrom(src => src.AppUser.FirstName))
                .ForCtorParam("LastName", x => x.MapFrom(src => src.AppUser.LastName));


            CreateMap<MealDto, Meal>().ReverseMap();

            CreateMap<MealScheduleDto, MealSchedule>()
                .ForMember(dest => dest.schedule,
                           opt => opt.MapFrom(src => new Schedule { StartDay = src.StartDate, EndDay = src.EndDate }));


            CreateMap<MuscleExerciseDto, MuscleExerices>();

            CreateMap<ExerciseScheduleDto, ExercisesSchedule>()
                .ForMember(dest => dest.schedule,
                           opt => opt.MapFrom(src => new Schedule { StartDay = src.StartDate, EndDay = src.EndDate }));


            #region Address & Location
            CreateMap<AddressDto, Address>();
            CreateMap<LocationDto, Location>();
            #endregion
        }
    }
}
