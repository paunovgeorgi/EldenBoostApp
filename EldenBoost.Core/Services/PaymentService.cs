﻿using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Payment;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EldenBoost.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository repository;
        public PaymentService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<PaymentListViewModel>> AllPaymentsFiltered(Expression<Func<Payment, bool>> predicate)
        {
            var paymentsQuery = repository.AllReadOnly<Payment>()
              .Where(predicate)
              .Include(p => p.Booster)
              .Select(p => new PaymentListViewModel()
              {
                  Id = p.Id,
                  BoosterId = p.BoosterId,
                  BoosterName = p.Booster.User.Nickname,
                  Amount = p.Amount,
                  IsPaid = p.IsPaid,
                  Orders = p.Orders.Select(o => o.Id).ToList(),
                  IssueDate = p.IssueDate.ToShortDateString(),
              })
              .OrderByDescending(x => x.Id);

            var payments = await paymentsQuery.ToListAsync();

            return payments;
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

        public async Task<bool> HasOrdersToRequestAsync(string userId)
        {
            return await repository.AllReadOnly<Booster>()
                .Where(b => b.UserId == userId)
                .AnyAsync(b => b.Orders.Any(o => o.IsPaid == false && o.Status == "Completed"));
        }

        public async Task<bool> IsPendingAsync(string userId)
        {
            return await repository.AllReadOnly<Payment>()
                .AnyAsync(p => p.IsPaid == false && p.Booster.UserId == userId);
        }

        public async Task<decimal> ReadyForRequstAsync(string userId)
        {
            return await repository.AllReadOnly<Order>()
            .Include(o => o.Booster)
            .Where(o => o.Booster!.UserId == userId && o.IsPaid == false && o.Status == "Completed" && o.IsAddedToPayment == false)
            .SumAsync(o => o.BoosterPay);
        }

        public async Task<decimal> RequsetedAmountAsync(string userId)
        {
            return await repository.AllReadOnly<Payment>()
            .Where(p => p.IsPaid == false && p.Booster.UserId == userId)
            .Select(p => p.Amount)
            .FirstOrDefaultAsync();
        }

    }
}
