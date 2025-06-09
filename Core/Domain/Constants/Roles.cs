using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public abstract class Roles
    {
        public const string Admin = nameof(Admin);
        public const string Owner = nameof(Owner);
        public const string Trainee = nameof(Trainee);
        public const string Coach = nameof(Coach);
    }
}
