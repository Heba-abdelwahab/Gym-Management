using AutoMapper;
using Domain.Entities;
using Shared;

namespace Services.MappingProfiles
{
    public sealed class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, ClassToReturnDto>()
                .ForCtorParam("GymName", opt => opt.MapFrom(src => src.Gym.Name))
                .ForCtorParam("CoachName", opt => opt.MapFrom(src => $"{src.Coach.AppUser.FirstName} {src.Coach.AppUser.LastName}"));

            CreateMap<ClassDto, Class>();
        }

    }
}
