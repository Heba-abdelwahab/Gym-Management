using AutoMapper;
using Domain.Entities;
using Domain.Entities.Chat;
using Domain.ValueObjects;
using Domain.ValueObjects.Chat;
using Domain.ValueObjects.member;
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

            #region Diet
            // From DTO to Entity
            CreateMap<MealDto, Meal>().ReverseMap();
            CreateMap<MealResultDto, Meal>().ReverseMap();

            CreateMap<MealScheduleDto, MealSchedule>()
                .ForMember(dest => dest.schedule, opt => opt.MapFrom(src => new Schedule { StartDay = src.StartDate, EndDay = src.EndDate }));

            CreateMap<MealScheduleUpdateDto, MealSchedule>()
                .ForMember(dest => dest.schedule, opt => opt.MapFrom(src => new Schedule { StartDay = src.StartDate, EndDay = src.EndDate }));

            // From Entity to DTO
            CreateMap<MealSchedule, MealScheduleResultDto>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.schedule.StartDay))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.schedule.EndDay));
            #endregion

            #region Exercise Schedule
            // --- From DTO to Entity ---
            CreateMap<ExerciseResultDto, Exercise>().ReverseMap();
            CreateMap<MuscleResultDto, Muscle>().ReverseMap();
            CreateMap<ScheduledExerciseDto, ScheduledExercise>().ReverseMap();
            CreateMap<ScheduledExerciseUpdateDto, ScheduledExercise>().ReverseMap();

            CreateMap<ScheduledExerciseResultDto, ScheduledExercise>().ReverseMap();

            CreateMap<ExerciseScheduleDto, ExercisesSchedule>()
                .ForMember(dest => dest.schedule,
                           opt => opt.MapFrom(src => new Schedule { StartDay = src.StartDate, EndDay = src.EndDate }));

            CreateMap<ExerciseScheduleUpdateDto, ExercisesSchedule>()
               .ForMember(dest => dest.schedule,
                          opt => opt.MapFrom(src => new Schedule { StartDay = src.StartDate, EndDay = src.EndDate }));

            // --- From Entity to DTO ---

            CreateMap<ExercisesSchedule, ExerciseScheduleResultDto>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.schedule.StartDay))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.schedule.EndDay));


            #endregion

            #region Address & Location
            CreateMap<AddressDto, Address>();
            CreateMap<LocationDto, Location>();
            #endregion


            #region DateTime

            CreateMap<DateTime, DateTime>()
                .ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));

            CreateMap<DateTime?, DateTime?>()
                .ConvertUsing(d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);


            #endregion


            #region Message

            CreateMap<Message, MessageDto>()
           .ForMember(dest => dest.SenderPhotoUrl,
            opt => opt.MapFrom(
               src => src.Sender.Photos.FirstOrDefault(user => user.IsMain)!.Url))
           .ForMember(dest => dest.RecipientPhotoUrl,
            opt => opt.MapFrom(
               src => src.Recipient.Photos.FirstOrDefault(user => user.IsMain)!.Url));


            #endregion


            #region Members & Photo 
            CreateMap<Photo, PhotoDto>();

            CreateMap<AppUser, MemberDto>()
             .ForMember(dest => dest.PhotoUrl,
                         opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url));
            //.ForMember(dest => dest.Age,
            //            opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            #endregion
        }
    }
}
