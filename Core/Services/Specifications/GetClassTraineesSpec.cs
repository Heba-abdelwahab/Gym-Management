using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetClassTraineesSpec:SpecificationBase<Class,int>
    {
        public GetClassTraineesSpec(int classId):base(c => c.Id == classId)
        { 
            AddIncludes("Trainees.AppUser"); 
        }
    }
}
