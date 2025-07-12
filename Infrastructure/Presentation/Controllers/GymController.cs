using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System.Text.Json;

namespace Presentation.Controllers
{
    public class GymController:ApiControllerBase
    {
        private readonly IServiceManager serviceManager;

        public GymController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet("{gymId:int}")]
        public async Task<ActionResult> GetGymById(int gymId)
        {
            var gymDto= await serviceManager.GymService.GetGymById(gymId);
            return Ok(gymDto);
        }

        [HttpGet("GymWithFeatures/{gymId:int}")]
        public async Task<ActionResult> GetGymWithFeaturesById(int gymId)
        {
            var gymGetDto = await serviceManager.GymService.GetGymWithFeaturesById(gymId);
            return Ok(gymGetDto);
        }

        [HttpPost("RequestAddGym")]
        public async Task<ActionResult> RequestAddGym([FromForm] GymWithFilesDto gymWithFilesDto)
        {
            var gymDto = JsonSerializer.Deserialize<GymDto>(gymWithFilesDto.gymInfo , new JsonSerializerOptions { PropertyNameCaseInsensitive=true});

            TryValidateModel(gymDto);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await serviceManager.GymService.RequestAddGym(gymWithFilesDto, gymDto);
            return Ok();
        }

        [HttpPut("{gymId:int}")]
        public async Task<ActionResult> UpdateGym(int gymId, [FromForm] GymWithFilesUpdate gymWithFilesUpdate)
        {
            var gymDto = JsonSerializer.Deserialize<GymUpdateDto>(gymWithFilesUpdate.gymInfo , new JsonSerializerOptions { PropertyNameCaseInsensitive=true});
            TryValidateModel(gymDto);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await serviceManager.GymService.UpdateGym(gymId, gymWithFilesUpdate, gymDto);
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





        [HttpGet("GetGymTypes")]
        public ActionResult GetGymTypes()
        {
            var gymTypes = serviceManager.GymService.GetGymTypes();
            return Ok(gymTypes);
        }

        [HttpGet("GetGymFeatures")]
        public async Task< ActionResult> GetGymFeatures()
        {
            var features = await serviceManager.GymService.GetGymFeatures();
            return Ok(features);
        }
        [HttpGet("Features/{gymId:int}")]
        public async Task<ActionResult> GetFeaturesByGymId(int gymId)
        {
            var features = await serviceManager.GymService.GetFeaturesByGymId(gymId);
            return Ok(features);
        }


        [HttpGet("Feature/{gymFeatureId:int}")]
        public async Task<ActionResult> GetGymFeatureById(int gymFeatureId)
        {
            var features = await serviceManager.GymService.GetGymFeatureById(gymFeatureId);
            return Ok(features);
        }

        [HttpPost("NonExGymFeature/{gymId:int}")]
        public async Task<ActionResult> AddNonExGymFeature([FromRoute] int gymId,[FromForm] NonExGymFeatureDto NonExGymFeatureDto)
        {
            var featureGymDto= await serviceManager.GymService.AddNonExGymFeature(gymId,NonExGymFeatureDto);
            return CreatedAtAction(nameof(GetGymFeatureById),new { gymFeatureId  = featureGymDto.Id },featureGymDto);
        }

        [HttpPost("ExtraGymFeature/{gymId:int}")]
        public async Task<ActionResult> AddExtraGymFeature([FromRoute] int gymId, [FromForm] ExGymFeatureDto gymFeatureDto)
        {
            var featureGymDto =  await serviceManager.GymService.AddExtraGymFeature(gymId, gymFeatureDto);
            return CreatedAtAction(nameof(GetGymFeatureById), new { gymFeatureId = featureGymDto.Id }, featureGymDto);

        }

        [HttpPut("GymFeature/{gymFeatureId:int}")]
        public async Task<ActionResult>  UpdateGymFeature(int gymFeatureId, [FromForm] GymFeaturePutDto GymFeaturePutDto)
        {
            var updatedGymFeature= await serviceManager.GymService.UpdateGymFeature(gymFeatureId, GymFeaturePutDto);
            return Ok(updatedGymFeature);
        }

        [HttpDelete("GymFeature/{gymFeatureId:int}")]
        public async Task<ActionResult> DeleteGymFeature(int gymFeatureId)
        {
            await serviceManager.GymService.DeleteGymFeature(gymFeatureId);
            return Ok(gymFeatureId);
        }

        [HttpGet("PendingGyms")]
        public async Task<ActionResult> GetPendingGyms()
        {
            var pendingGymsDto = await serviceManager.GymService.GetPendingGyms();
            return Ok(pendingGymsDto);
        }

        [HttpGet("HandleGymAddRequest/{gymId:int}")]
        public async Task<ActionResult> HandleGymAddRequest(int gymId, bool IsAccepted)
        {
            await serviceManager.GymService.HandleGymAddRequest(gymId, IsAccepted);
            return Ok(gymId);
        }

    }
}
