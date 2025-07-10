using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    //for deleting gym
    public enum DeleteStatus
    {
        NotRequested = 0, 
        Pending = 1,      
        Accepted = 2,     
        Rejected = 3      
    }
}
