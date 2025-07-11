using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class GymController:ApiControllerBase
    {
        private readonly IServiceManager serviceManager;

        public GymController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }


        [HttpPost("requestAddGym")]
        public async Task<ActionResult> RequestAddGym(GymDto gymDto)
        {
            await serviceManager.GymService.RequestAddGym(gymDto);
            return Ok();
        }
        [HttpGet("{GymID:int}")]

        public async Task<ActionResult> GetGymFeature(int GymID)
        {
            var result= await serviceManager.GymService.GetGymFeatures(GymID);
            if(result !=null)
            return Ok(result);

            return BadRequest("Failed when Trying Get Features.");

        }


        [HttpPost("MemberShip")]
        public async Task<ActionResult> AddMemberShip(MemberShipReturnDto member)
        {
           var added= await serviceManager.GymService.GymMemberShip(member);

            if (added)
                return Ok(new { message = "Added MemberShip Successfully" });

            else return BadRequest(new { error = "Failed when Trying to Add MemberShip." });
        }
        [HttpPut("UpdateMemberShip/{MemberID:int}")]
public async Task<ActionResult> UpdateMemberShip(int MemberID, MemberShipReturnDto member)
{
    var Updated = await serviceManager.GymService.UpdateGymMemberShip(MemberID, member);

    if (Updated)
        return Ok(new { message = "Update MemberShip Successfully" }); // أعد كـ JSON
    else 
        return BadRequest(new { message = "Failed when Trying to Update MemberShip." });
}
        [HttpDelete("DeleteMemberShip/{memberID:int}")]
        public async Task<ActionResult> DeleteMemberShip(int memberID)
        {
            var Deleted = await serviceManager.GymService.DeleteGymMemberShip(memberID);

            if (Deleted)
                return Ok(new { message = "Delete MemberShip Successfully" }); // Return JSON
            else
                return BadRequest(new { message = "Failed when Trying to Delete MemberShip." });
        }

        [HttpGet("GetmMemberShip/{MemberID:int}")]
        public async Task<ActionResult> GetMembershipByID(int MemberID)
        {
            var Membership = await serviceManager.GymService.GetGymMemberShipById(MemberID);

            if (Membership!=null)
                return Ok(Membership);

            else return BadRequest("Failed when Trying to Get MemberShip.");
        }


        [HttpGet("GetmMemberShips/{GymID:int}")]
        public async Task<ActionResult> GetMembershipsByGym(int GymID)
        {
            var Memberships = await serviceManager.GymService.GetGymMemberShips(GymID);

            if (Memberships.Any())
                return Ok(Memberships);

            else return BadRequest("Failed when Trying to Get MemberShips.");
        }




    }
}
