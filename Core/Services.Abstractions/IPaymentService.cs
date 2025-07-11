using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IPaymentService
    {
        Task<string?> ProcessPaymentAsync(int traineeId, decimal amount, PayFor payFor, int serviceId);
    }
}
