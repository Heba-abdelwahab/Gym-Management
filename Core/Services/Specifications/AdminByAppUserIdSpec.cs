using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class AdminByAppUserIdSpec:SpecificationBase<Admin,int>
    {
        public AdminByAppUserIdSpec(string appUserId) : base(a => a.AppUserId == appUserId)
        {

        }
    }
}
