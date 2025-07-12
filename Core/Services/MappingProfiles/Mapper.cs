using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Chat;
using Domain.ValueObjects;
using Domain.ValueObjects.Chat;
using Domain.ValueObjects.member;
using Shared;
using Shared.coach;
using Shared.Trainee;

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

            #region Diet
            // From DTO to Entity
            CreateMap<MealDto, Meal>().ReverseMap();
            CreateMap<MealResultDto, Meal>().ReverseMap();

            // MealSchedule mappings
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


            #region coach

            CreateMap<Coach, CoachInfoResultDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.Specializations, opt => opt.MapFrom(src => src.Specializations.ToString()))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Image.Url))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
            src.DateOfBirth.HasValue ?
            (int)((DateTime.Now - src.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)).TotalDays / 365.25)
            : 0));

            #endregion


            #region Trainee

            CreateMap<Trainee, TraineeInfoResultDto>()
           .ForMember(dest => dest.FirstName,
               opt => opt.MapFrom(src => src.AppUser.FirstName))

           .ForMember(dest => dest.LastName,
               opt => opt.MapFrom(src => src.AppUser.LastName))

           .ForMember(dest => dest.UserName,
               opt => opt.MapFrom(src => src.AppUser.UserName))

           .ForMember(dest => dest.PhotoUrl,
               opt => opt.MapFrom(src => src.ImageUrl))

           .ForMember(dest => dest.ReasonForJoining,
               opt => opt.MapFrom(src => src.ReasonForJoining))

           .ForMember(dest => dest.Weight,
               opt => opt.MapFrom(src => src.Weight.HasValue ? src.Weight.Value.ToString("F1") + " KG" : "Not Set"))

           .ForMember(dest => dest.Gym,
               opt => opt.MapFrom(src => src.Gym != null ? src.Gym.Name : "No Gym"))

           .ForMember(dest => dest.Coach,
               opt => opt.MapFrom(src => src.Coach != null
                   ? $"{src.Coach.AppUser.FirstName} {src.Coach.AppUser.LastName}"
                   : "No Coach"))

           .ForMember(dest => dest.Age,
               opt => opt.MapFrom(src =>
                   src.DateOfBirth.Value.CalculateAge()))

           .ForMember(dest => dest.MembershipStartDate,
               opt => opt.MapFrom(src => src.MembershipStartDate))

           .ForMember(dest => dest.Address,
               opt => opt.MapFrom(src => src.Address));


            #endregion

        }
    }
}