using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services.MappingProfiles
{
    public sealed class GymOwnerProfile:Profile
    {
        public GymOwnerProfile()
        {
            CreateMap<GymOwner, GymOwnerToReturnDto>();
            CreateMap<GymOwner, List<GymOwnerDataDto>>()
     .ConvertUsing(src => src.Gyms.Select(g => new GymOwnerDataDto
     {
         Name = g.Name,
         GymType = g.GymType.ToString(),
         MembershipsCount = g.Memberships.Count,
         CoachesCount = g.GymCoaches.Count,
         FeaturesCount = g.GymFeatures.Count,
         TraineesCount = g.Trainees.Count,
         ClassesCount = g.Classes.Count
     }).ToList());

            CreateMap<GymOwner, List<OwnerMembershipdto>>()
         .ConvertUsing(src => src.Gyms
             .Select(g => new OwnerMembershipdto
             {
                 GymName = g.Name,
                 MembershipName = g.Memberships
                     .OrderByDescending(m => m.Trainees.Count)
                     .FirstOrDefault().Name,
                 TraineesCount = g.Memberships
                     .Max(m => m.Trainees.Count)
             })
             .ToList());


        }
    }
}
