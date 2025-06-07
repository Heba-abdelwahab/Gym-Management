using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ClassNotFoundException : NotFoundException
    {
        public ClassNotFoundException(int id) : base($"Class with Id: {id} was Not Found.")
        {
        }
    }
}
