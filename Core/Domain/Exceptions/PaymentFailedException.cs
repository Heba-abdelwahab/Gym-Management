using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class PaymentFailedException : BadRequestException
    {
        public PaymentFailedException(string Message) : base(Message)
        {
        }
    }
}
