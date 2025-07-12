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
            //CreateMap<GymDto, Gym>()
            //.ForMember(des => des.GymFeatures,
            //    opt => opt.MapFrom(src => src.GymFeatures.Select(gf => new GymFeature()
            //    {
            //        Cost = gf.Cost,
            //        Description = gf.Description,
            //        Image = gf.Image,
            //        FeatureId = gf.FeatureId
            //    })
            //    .Concat(src.GymExtraFeatures.Select(gef => new GymFeature()
            //    {
            //        Cost = gef.Cost,
            //        Description = gef.Description,
            //        Image = gef.Image,
            //        Feature = new Feature() { Name = gef.Name, IsExtra = true }

            //    }))
            //    )
            //);
            CreateMap<GymDto, Gym>()
                .ForMember(des => des.GymFeatures,
                    opt => opt.MapFrom((src, dest, destMember, context) =>
                    {
                        var exFeatureImages = ((IEnumerable<PhotoUploadedResult>)context.Items["exFeatureImages"])?.ToList();
                        var FeatureImages = ((IEnumerable<PhotoUploadedResult>)context.Items["FeatureImages"])?.ToList();

                        return src.GymFeatures.Select((gf, idx) =>
                        {
                            var photResult = new MediaValueObj()
                            {
                                Url = FeatureImages[idx].ImageName,
                                PublicId = FeatureImages[idx].PublicId,
                                Type = MediaType.Image
                            };
                            return new GymFeature()
                            {
                                Cost = gf.Cost,
                                Description = gf.Description,
                                Image = photResult,
                                FeatureId = gf.FeatureId
                            };
                        })
                        .Concat(src.GymExtraFeatures.Select((gef, idx) =>
                        {
                            var photResult = new MediaValueObj()
                            {
                                Url = exFeatureImages[idx].ImageName,
                                PublicId = exFeatureImages[idx].PublicId,
                                Type = MediaType.Image
                            };

                            return new GymFeature()
                            {
                                Cost = gef.Cost,
                                Description = gef.Description,
                                Image = photResult,
                                Feature = new Feature()
                                {
                                    Name = gef.Name,
                                    IsExtra = true
                                }
                            };
                        }));
                    }));


            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<LocationDto,Location>().ReverseMap();
            CreateMap<GymFeature, GymFeatureDto>()
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Feature.Name))
                .ForMember(des => des.Image, opt => opt.MapFrom((src,dest,destMemeber,context) => context.Items["CloudinaryBaseUrl"] +src.Image.Url))
                .ForMember(des=>des.IsExtra,opt=>opt.MapFrom(src=>src.Feature.IsExtra));
                
            CreateMap<GymUpdateDto, Gym>();
            CreateMap<NonExGymFeatureDto, GymFeature>()
                .ForMember(des=>des.Image,opt=>opt.Ignore());
            CreateMap<ExGymFeatureDto,GymFeature>()
                .ForMember(des=>des.Feature,opt=>opt.MapFrom(src=>new Feature() { Name = src.Name , IsExtra = true}))
                .ForMember(des => des.Image, opt => opt.Ignore());
            CreateMap<GymFeaturePutDto, GymFeature>()
                .ForMember(des=>des.Image,opt=>opt.Ignore());
            CreateMap<Gym, GymGetDto>()
                .ForMember(des=>des.MediaUrl,opt=>opt.MapFrom(src=>src.Media.Url))
                .ForMember(des => des.GymImagesUrl, opt=>opt.MapFrom(src=>src.Images.Select(img=>img.MediaValue.Url).ToList()))
                .ForMember(des=>des.GymTypeValue, opt => opt.MapFrom(src => Enum.GetName(typeof(GymType), src.GymType) ));
            CreateMap<GymUpdateDto, Gym>();

            CreateMap<Address, AddressToReturnDto>().ReverseMap();
            CreateMap<Gym, GymToReturnDto>()
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Media));


            CreateMap<AddressDto, Address>().ReverseMap();
           // CreateMap<GymExtraFeatureDto, GymFeature>()
           //.ForMember(des=>des.Feature, opt => opt.MapFrom(src => new Feature() { Name = src.Name, IsExtra = true }));

            CreateMap<GymFeature, GymFeatureReturnDto>().ForMember(des => des.FeatureName, opt =>
            opt.MapFrom(src => src.Feature.Name));
                

            CreateMap<MemberShipReturnDto, Membership>()
    .ForMember(dest => dest.Features, opt => opt.Ignore());

            CreateMap<Membership, MemberShipDto>()
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features));
            CreateMap<Feature, ItemDto > ();

            CreateMap<Gym, PendingGymDto>()
                .ForMember(des => des.GymId, opt => opt.MapFrom(src => src.Id))
                .ForMember(des=>des.GymType, opt=>opt.MapFrom(src=>Enum.GetName(typeof(GymType),src.GymType)));
           
        }
    }
}
