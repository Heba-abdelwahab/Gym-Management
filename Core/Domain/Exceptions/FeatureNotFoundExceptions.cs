using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class FeatureNotFoundExceptions:NotFoundException
    {
        public FeatureNotFoundExceptions(int featureId) : base($"Feature with Id: {featureId} was Not Found.")
        {
            
        }
    }
}
