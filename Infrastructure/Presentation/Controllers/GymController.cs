using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

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

        [HttpPost("RequestAddGym")]
        public async Task<ActionResult> RequestAddGym(GymDto gymDto)
        {
            await serviceManager.GymService.RequestAddGym(gymDto);
            return Ok();
        }

        [HttpPut("{gymId:int}")]
        public async Task<ActionResult> UpdateGym(int gymId, GymUpdateDto gymUpdateDto)
        {
            await serviceManager.GymService.UpdateGym(gymId, gymUpdateDto);
            return Ok();
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
        [HttpGet("GetFeaturesByGymId/{gymId:int}")]
        public async Task<ActionResult> GetGymFeatures(int gymId)
        {
            var features = await serviceManager.GymService.GetFeaturesByGymId(gymId);
            return Ok(features);
        }


        [HttpGet("GetGymFeatureById/{gymFeatureId:int}")]
        public async Task<ActionResult> GetGymFeatureById(int gymFeatureId)
        {
            var features = await serviceManager.GymService.GetGymFeatureById(gymFeatureId);
            return Ok(features);
        }

        [HttpPost("NonExGymFeature/{gymId:int}")]
        public async Task<ActionResult> AddNonExGymFeature(int gymId, NonExGymFeatureDto NonExGymFeatureDto)
        {
            await serviceManager.GymService.AddNonExGymFeature(gymId,NonExGymFeatureDto);
            return Ok();
        }

        [HttpPost("ExtraGymFeature/{gymId:int}")]
        public async Task<ActionResult> AddExtraGymFeature(int gymId, ExGymFeatureDto gymFeatureDto)
        {
            await serviceManager.GymService.AddExtraGymFeature(gymId, gymFeatureDto);
            return Ok();
        }

        [HttpPut("GymFeature/{gymFeatureId:int}")]
        public async Task<ActionResult>  UpdateGymFeature(int gymFeatureId, GymFeaturePutDto GymFeaturePutDto)
        {
            await serviceManager.GymService.UpdateGymFeature(gymFeatureId, GymFeaturePutDto);
            return Ok();
        }

        [HttpDelete("GymFeature/{gymFeatureId:int}")]
        public async Task<ActionResult> DeleteGymFeature(int gymFeatureId)
        {
            await serviceManager.GymService.DeleteGymFeature(gymFeatureId);
            return Ok();
        }
    }
}
