using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record AdminDashboardDto(int NumGyms, int NumGymOwners, int NumCoaches, int NumTrainees, int NumPendingGyms, List<GymOwnerStatDto> OwnerStatDto);
}
