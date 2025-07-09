using AutoMapper;
using Domain.Entities;
using Shared;
using Shared.TraineeGym;

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
            CreateMap<Trainee, Shared.ClassTraineeToReturnDto>()
                .ForCtorParam("FirstName", opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForCtorParam("LastName", opt => opt.MapFrom(src => src.AppUser.LastName))
                .ForCtorParam("PhoneNumber", opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id));

            // Mapping for ClassTraineeToReturnDto and CurrentTraineesCount From Trainees collection
            CreateMap<Class, Shared.TraineeGym.ClassTraineeToReturnDto>()
                .ForMember(dest => dest.CurrentCapacity, opt => opt.MapFrom(src => src.Trainees.Count));


        }

    }
}
