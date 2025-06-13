using AutoMapper;
using Domain.Entities;
using Shared;
using Shared.TraineeGym;

namespace Services.MappingProfiles
{
    internal class CoachProfiler : Profile
    {
        public CoachProfiler()
        {
            CreateMap<Coach, CoachPendingDto>()
           .ForCtorParam("FirstName", opt => opt.MapFrom(src => src.AppUser.FirstName))
           .ForCtorParam("LastName", opt => opt.MapFrom(src => src.AppUser.LastName))
           .ForCtorParam("Specializations", opt => opt.MapFrom(src => src.Specializations.ToString()))
           .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
           .ReverseMap();

            CreateMap<Coach, TraineeCoachToReturnDto>()
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName));
        }
    }
}
