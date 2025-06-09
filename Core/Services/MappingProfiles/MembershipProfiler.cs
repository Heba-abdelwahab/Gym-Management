using AutoMapper;
using Domain.Entities;
using Shared.TraineeGym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class MembershipProfiler : Profile
    {
        public MembershipProfiler()
        {
            CreateMap<GymFeature, FeatureDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Feature.Name));

            CreateMap<Membership, GymMembershipsDto>();
        }
    }
}
