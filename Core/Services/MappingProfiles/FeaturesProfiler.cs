﻿using AutoMapper;
using Domain.Entities;
using Shared;
using Shared.TraineeGym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class FeaturesProfiler : Profile
    {
        public FeaturesProfiler()
        {
            CreateMap<GymFeature, GymFeatureToReturnDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Feature.Name))
                .ForMember(dest => dest.IsExtra, opt => opt.MapFrom(src => src.Feature.IsExtra))
                .ReverseMap();

            CreateMap<TraineeSelectedFeature, TraineeFeatureToReturnDto>()
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.SessionCount))
                .ForMember(dest => dest.SessionCost, opt => opt.MapFrom(src => src.GymFeature.Cost))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GymFeature.Feature.Name));

        }
    }
}
