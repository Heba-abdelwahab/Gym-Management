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
        Task RequestAddGym(GymWithFilesDto gymWithFilesDto, GymDto gymDto);
        IEnumerable<ItemDto> GetGymTypes();
        Task<IEnumerable<ItemDto>> GetGymFeatures();
        Task UpdateGym(int gymId, GymWithFilesUpdate gymWithFilesUpdate, GymUpdateDto gymUpdateDto);
        Task<GymGetDto> GetGymById(int gymId);
        Task <IEnumerable<GymFeatureDto>> GetFeaturesByGymId (int gymId);
        Task<GymFeatureDto> GetGymFeatureById(int gymFeatureId);
        Task AddNonExGymFeature(int gymId , NonExGymFeatureDto NonExGymFeatureDto);
        Task AddExtraGymFeature(int gymId, ExGymFeatureDto ExGymFeature);
        Task UpdateGymFeature(int gymFeatureId , GymFeaturePutDto GymFeaturePutDto);
        Task DeleteGymFeature(int gymFeatureId);


    }
}
