using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DietNotFoundException : NotFoundException
    {
        public DietNotFoundException(int dietId) : base($"Diet with ID {dietId} not found.")
        {
        }
    }
}
