using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record ClassToReturnDto(int Id, string Name, string Description, decimal Cost, int Capacity, int CurrentCapacity, DateTime Date, string GymName, string CoachName);
}
