using AutoMapper;
using AutoMapper.Features;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.TraineeGym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    internal class GymProfiler:Profile
    {
        public GymProfiler()
        {
            CreateMap<GymDto, Gym>()
            .ForMember(des => des.GymFeatures,
                opt => opt.MapFrom(src => src.GymFeatures.Select(gf => new GymFeature()
                {
                    Cost = gf.Cost,
                    Description = gf.Description,
                    Image = gf.Image,
                    FeatureId = gf.FeatureId
                })
                .Concat(src.GymExtraFeatures.Select(gef => new GymFeature()
                {
                    Cost = gef.Cost,
                    Description = gef.Description,
                    Image = gef.Image,
                    Feature = new Feature() { Name = gef.Name, IsExtra = true }

                }))
                )
            );
            //CreateMap<GymDto, Gym>()
            //.ForMember(des => des.GymFeatures,
            //    opt => opt.MapFrom(src => src.GymExtraFeatureDto.Select(gef => new GymFeature()
            //    {
            //        Cost = gef.Cost,
            //        Description = gef.Description,
            //        Image = gef.Image,
            //        Feature = gef.IsExtra ? new Feature() { Name = gef.Name, IsExtra = true } :null,
            //        FeatureId = gef.IsExtra ?0:gef.FeatureId.Value,
            //    })));

            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<GymExtraFeatureDto, GymFeature>()
           .ForMember(des=>des.Feature, opt => opt.MapFrom(src => new Feature() { Name = src.Name, IsExtra = true }));

            CreateMap<Address, AddressToReturnDto>();
            CreateMap<Gym, GymToReturnDto>()
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Media));
           
        }
    }
}
