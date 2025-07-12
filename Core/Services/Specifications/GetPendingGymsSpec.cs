using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class GetPendingGymsSpec: SpecificationBase<Gym, int>
    {
        public GetPendingGymsSpec():base(g=>g.AddGymStatus == RequestStatus.Pending)
        {
            
        }
    }
}
