using Domain.Contracts;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<string?> ProcessPaymentAsync(int traineeId, decimal amount, PayFor payFor, int serviceId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];


            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;


            var Options = new PaymentIntentCreateOptions()
            {
                Amount = (long) (amount * 100),
                Currency = "usd",
                PaymentMethodTypes = new List<string>() { "card" }
            };
            paymentIntent = await service.CreateAsync(Options);
            string ClientSecret = paymentIntent.ClientSecret;

            var payment = new Payments
            {
                Amount = amount,
                PaymentDate = DateTime.UtcNow,
                PaymentIntentId = paymentIntent.Id,
                PayFor = payFor,
                TraineeId = traineeId,
                ClientSecret = ClientSecret
            };

            _unitOfWork.GetRepositories<Payments,int>().Insert(payment);
            var result = await _unitOfWork.CompleteSaveAsync();

            if (result)
                return ClientSecret;

            return null;
        }
    }
}
