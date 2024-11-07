using EldenBoost.Core.Contracts;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository repository;
        public PaymentService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task CreatePaymentAsync(string userId)
        {
            Booster? booster = await repository.All<Booster>()
               .Where(b => b.UserId == userId)
               .Include(b => b.Orders)
               .FirstOrDefaultAsync();

            if (booster != null)
            {
                var orders = booster.Orders.Where(o => o.IsPaid == false && o.Status == "Completed").ToList();
                var amount = orders.Sum(o => o.BoosterPay);
                Payment payment = new Payment
                {
                    BoosterId = booster.Id,
                    Amount = amount,
                    IssueDate = DateTime.UtcNow
                };

                foreach (var order in orders)
                {
                    order.IsAddedToPayment = true;
                    payment.Orders.Add(order);
                }

                await repository.AddAsync<Payment>(payment);
                await repository.SaveChangesAsync();
            }
        }

        public async Task<bool> IsPendingAsync(string userId)
        {
            return await repository.AllReadOnly<Payment>()
                .AnyAsync(p => p.IsPaid == false && p.Booster.UserId == userId);
        }
    }
}
