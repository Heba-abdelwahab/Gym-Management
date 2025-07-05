using AutoMapper;
using AutoMapper.Features;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Shared;
using Shared.TraineeGym;

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
            CreateMap<LocationDto,Location>().ReverseMap();
            CreateMap<GymFeature, GymFeatureDto>()
                .ForMember(des=>des.Name, opt => opt.MapFrom(src => src.Feature.Name));
            CreateMap<GymUpdateDto, Gym>();
            CreateMap<NonExGymFeatureDto, GymFeature>();
            CreateMap<ExGymFeatureDto,GymFeature>()
                .ForMember(des=>des.Feature,opt=>opt.MapFrom(src=>new Feature() { Name = src.Name , IsExtra = true}));
            CreateMap<GymFeaturePutDto, GymFeature>();
            CreateMap<Gym, GymGetDto>();
            CreateMap<Address, AddressToReturnDto>();
            CreateMap<Gym, GymToReturnDto>()
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Media));

            CreateMap<Feature, ItemDto > ();
           
        }
    }
}
