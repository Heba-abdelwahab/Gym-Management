using Microsoft.EntityFrameworkCore.Metadata;
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
        Task RequestAddGym(GymDto gymDto);
        Task<List<GymFeatureReturnDto>> GetGymFeatures(int GymId);
        public Task<bool> GymMemberShip(MemberShipReturnDto memberShip);
        public Task<bool> UpdateGymMemberShip(int MemberID, MemberShipReturnDto memberShip);
        public Task<bool> DeleteGymMemberShip(int membershipID);

        public Task<MemberShipDto> GetGymMemberShipById(int MemberiId);


        public Task<List<MemberShipDto>> GetGymMemberShips(int GymID);

    }
}
