using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Contracts;

namespace Services.Specifications
{
    public class GetCairoUsersSpec:SpecificationBase<User,int>
    {
        public GetCairoUsersSpec():base(u=>u.address=="cairo")
        {
            ApplyOrderBy(u => u.name);
            ApplyTake(2);
        }

    }
}
