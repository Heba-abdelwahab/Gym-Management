using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class CoachDashboardProfile : Profile
    {
        public CoachDashboardProfile()
        {
           
            CreateMap<GymCoach, GymCoachDashboardToReturnDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Gym.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Gym.Name))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Gym.Media));

            CreateMap<Class, ClassCoachDashboardToReturnDto>();

            CreateMap<Trainee, TraineeCoachDashboardToReturnDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));


            CreateMap<Coach, CoachDashboardToReturnDto>()
                .ForMember(dest => dest.Gyms, opt => opt.MapFrom(src =>
                    src.GymCoaches.Where(gc => gc.Status == RequestStatus.Accepted)))

                .ForMember(dest => dest.Classes, opt => opt.MapFrom(src => src.Classes))

                .ForMember(dest => dest.Trainees, opt => opt.MapFrom(src => src.Trainees));
        }
    }
}
