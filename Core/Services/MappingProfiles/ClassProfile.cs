using AutoMapper;
using Domain.Entities;
using Shared;

namespace Services.MappingProfiles
{
    internal sealed class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, ClassToReturnDto>()
                .ForMember(dest => dest.CoachName, opt => opt.MapFrom(src => src.Coach.Name))
                .ForMember(dest => dest.GymName, opt => opt.MapFrom(src => src.Gym.Name));

            CreateMap<ClassDto, Class>();
        }
    }
}
