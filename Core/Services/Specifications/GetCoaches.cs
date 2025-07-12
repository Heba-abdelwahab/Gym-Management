using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
namespace Services.Specifications
{
    internal class GetCoaches:SpecificationBase<Coach,int>
    {

        public GetCoaches(int id) : base(c=>c.GymCoaches.Any(g=>g.GymId==id&&g.Status==RequestStatus.Accepted&&g.Capcity>g.CurrentCapcity))
        {
            AddIncludes(c => c.AppUser);
            AddIncludes(c=>c.GymCoaches) ;
              
        }

    }
}
