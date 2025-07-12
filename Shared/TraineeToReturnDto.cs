using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public  class TraineeToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MemberShipName { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
        public String CoachName { get; set; }
        public int CoachId { get; set; }

    }



}
