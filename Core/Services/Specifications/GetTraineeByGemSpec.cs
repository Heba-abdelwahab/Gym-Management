using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    internal class GetTraineeByGemSpec:SpecificationBase<Trainee,int>
    {

      public  GetTraineeByGemSpec(int gymid) :base(t=>t.GymId==gymid)
        { 
        }
    }
}
