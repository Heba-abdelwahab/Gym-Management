using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class GetGymWithImagesSpec:SpecificationBase<Gym,int>
    {
        public GetGymWithImagesSpec(int GymId):base(g=>g.Id==GymId)
        {
            AddIncludes(g=>g.Images);
        }
    }
}
