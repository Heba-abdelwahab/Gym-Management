using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Http;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IGymService
    {
        Task<List<GymFeatureReturnDto>> GetGymFeatures(int GymId);
        public Task<bool> GymMemberShip(MemberShipReturnDto memberShip);
        public Task<bool> UpdateGymMemberShip(int MemberID, MemberShipReturnDto memberShip);
        public Task<bool> DeleteGymMemberShip(int membershipID);

        public Task<MemberShipDto> GetGymMemberShipById(int MemberiId);


        public Task<List<MemberShipDto>> GetGymMemberShips(int GymID);

        Task<int> RequestAddGym(GymWithFilesDto gymWithFilesDto, GymDto gymDto);
        IEnumerable<ItemDto> GetGymTypes();
        Task<IEnumerable<ItemDto>> GetGymFeatures();
        Task UpdateGym(int gymId, GymWithFilesUpdate gymWithFilesUpdate, GymUpdateDto gymUpdateDto);
        Task<GymGetDto> GetGymById(int gymId);
        Task <IEnumerable<GymFeatureDto>> GetFeaturesByGymId (int gymId);
        Task<GymFeatureDto> GetGymFeatureById(int gymFeatureId);
        Task<GymFeatureDto> AddNonExGymFeature(int gymId , NonExGymFeatureDto NonExGymFeatureDto);
        Task<GymFeatureDto> AddExtraGymFeature(int gymId, ExGymFeatureDto ExGymFeature);
        Task<GymFeatureDto> UpdateGymFeature(int gymFeatureId , GymFeaturePutDto GymFeaturePutDto);
        Task DeleteGymFeature(int gymFeatureId);
        Task<IEnumerable<PendingGymDto>> GetPendingGyms();
        Task<int> HandleGymAddRequest(int gymId, bool IsAccepted);
        Task<GymGetDto> GetGymWithFeaturesById(int gymId);


    }
}
