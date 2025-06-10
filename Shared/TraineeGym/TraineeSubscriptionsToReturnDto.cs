using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TraineeGym
{
    public class TraineeSubscriptionsToReturnDto
    {
        public DateTime? MembershipStartDate { get; set; }
        public DateTime? MembershipEndDate { get; set; }
        public GymToReturnDto GymData { get; set; }
        public GymMembershipsDto Membership { get; set; }
        public IReadOnlyList<TraineeFeatureToReturnDto> Features { get; set; } = new List<TraineeFeatureToReturnDto>();
        public IReadOnlyList<ClassTraineeToReturnDto> Class { get; set; } = new List<ClassTraineeToReturnDto>();
    }
}
