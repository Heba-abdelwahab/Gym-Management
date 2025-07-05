using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class GetDietByIdSpec : SpecificationBase<MealSchedule, int>
    {
        public GetDietByIdSpec(int dietId) : base(d => d.Id == dietId)
        {
            AddIncludes(d => d.Meals);
        }
    }
}
