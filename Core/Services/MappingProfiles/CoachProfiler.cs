using AutoMapper;
using Domain.Entities;
using Shared;

namespace Services.MappingProfiles
{
    internal class CoachProfiler:Profile
    {
        public CoachProfiler()
        {
            // CreateMap<Coach,CoachPendingDto>()
            //.ForCtorParam("FirstName", opt=>opt.MapFrom(src=>src.AppUser.FirstName))
            //.ForCtorParam("LastName", opt=>opt.MapFrom(src=>src.AppUser.LastName))
            //.ForCtorParam("Specializations", opt=>opt.MapFrom(src=>src.Specializations.ToString()))
            //.ForCtorParam("Id", opt=>opt.MapFrom(src=>src.Id))
            //.ForCtorParam("CVUrl",opt=>opt.MapFrom(src=>src.CV.Url))
            //.ForCtorParam("ImageUrl",opt=>opt.MapFrom(src=>src.Image.Url))
            //.ReverseMap();

            CreateMap<WorkDay, WorkDayPendingCoachDto>()
                .ForMember(des=>des.Day , opt=>opt.MapFrom(src=>Enum.GetName(typeof(DayOfWeek), src.Day) ))
                .ForMember(des => des.Start, opt => opt.MapFrom(src => src.Start.ToString()))
                .ForMember(des => des.End, opt => opt.MapFrom(src => src.End.ToString()));

            CreateMap<GymCoach, CoachPendingDto>()
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Coach.AppUser.FirstName + ' ' + src.Coach.AppUser.LastName))
                .ForMember(des => des.Specializations, opt => opt.MapFrom(src => src.Coach.Specializations))
                .ForMember(des => des.About, opt => opt.MapFrom(src => src.Coach.About))
                .ForMember(des => des.ApplicationCVUrl, opt => opt.MapFrom(src => src.Coach.CV.Url))
                .ForMember(des => des.ImageUrl, opt => opt.MapFrom(src => src.Coach.Image.Url));
        }
    }
}
