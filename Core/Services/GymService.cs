using AutoMapper;
using AutoMapper.Execution;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GymService: IGymService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GymService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteGymMemberShip(int membershipID)
        {

            var membership = await unitOfWork.GetRepositories<Membership, int>().GetByIdWithSpecAsync(new GetMemberShipSpec(membershipID));
            if (membership == null) throw  new MemberShipNotFoundException(membershipID);
             unitOfWork.GetRepositories<Membership,int>().Delete(membership);
            var result=await unitOfWork.CompleteSaveAsync();
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<List<GymFeatureReturnDto>> GetGymFeatures(int GymId)
        {
            var Features =await unitOfWork.GetRepositories<GymFeature, int>()
                .GetAllWithSpecAsync(new GetGymFeatureSpec(GymId));
            if (!Features.Any())
            {
                throw new GymFeatureNotFoundException(GymId);
            }
            var result= mapper.Map<List<GymFeatureReturnDto>>(Features);

            return result;
        }

        public async Task<MemberShipDto> GetGymMemberShipById(int MemberiId)
        {

            var MemberShip =await unitOfWork.GetRepositories<Membership, int>().GetByIdWithSpecAsync(new GetMemberShipSpec(MemberiId));
            if (MemberShip == null)
            {
                throw new MemberShipNotFoundException(MemberiId);
            }

            var Mapping = mapper.Map<MemberShipDto>(MemberShip);

            return Mapping;

        }

        public async Task<List<MemberShipDto>> GetGymMemberShips(int GymID)
        {
            var memerships =await unitOfWork.GetRepositories<Membership, int>()
                .GetAllWithSpecAsync(new GetMemberShipsByGymDtoSpec(GymID));
          var  mappingList=mapper.Map <List<MemberShipDto>> (memerships);
            if(!mappingList.Any())
                throw new MemberShipInGymNotFoundException(GymID);

            else
                return mappingList;
        }

        public async Task<bool> GymMemberShip(MemberShipReturnDto memberShip)
        {
           
            var MapingMemberShip=mapper.Map<Membership>(memberShip);
         
            

            foreach (var featureId in memberShip.GymFeaturesId)
            {
                var gymfeature = await unitOfWork.GetRepositories<GymFeature,int>().GetByIdWithSpecAsync(new GymFeatureSpec(featureId));
                if (gymfeature != null)
                {
                    MapingMemberShip.Features.Add(gymfeature);
                }
            }

            unitOfWork.GetRepositories<Membership, int>().Insert(MapingMemberShip);
      var result= await  unitOfWork.CompleteSaveAsync();
            if (result) return true;
            return false;

        }

        public async Task RequestAddGym (GymDto gymDto)
        {
            Gym gym =  mapper.Map<Gym>(gymDto);
            gym.Media = "f";
            unitOfWork.GetRepositories<Gym,int>().Insert(gym);
            await unitOfWork.CompleteSaveAsync();
        }

        public async Task<bool> UpdateGymMemberShip(int MemberID, MemberShipReturnDto memberShip)
        {
           
            var MapingMemberShip =await unitOfWork.GetRepositories<Membership,int>()
                .GetByIdWithSpecAsync(new GetMemberShipSpec(MemberID));
      

            MapingMemberShip.Name= memberShip.Name;
            MapingMemberShip.Cost = memberShip.Cost;
            MapingMemberShip.Description = memberShip.Description;
            MapingMemberShip.Duration = memberShip.Duration;
            MapingMemberShip.Count = memberShip.Count;
           MapingMemberShip.Features.Clear();

            foreach (var featureId in memberShip.GymFeaturesId)
            {
                var gymfeature = await unitOfWork.GetRepositories<GymFeature, int>().
                    GetByIdWithSpecAsync(new GymFeatureSpec(featureId));
                if (gymfeature != null)
                {
                    MapingMemberShip.Features.Add(gymfeature);
                }
            }

            unitOfWork.GetRepositories<Membership, int>().Update(MapingMemberShip);
            var result = await unitOfWork.CompleteSaveAsync();
            if (result) return true;
            return false;
        }
    }
}
