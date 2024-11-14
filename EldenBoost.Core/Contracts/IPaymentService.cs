using EldenBoost.Core.Models.Payment;
using EldenBoost.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(string userId);
        Task<bool> IsPendingAsync(string userId);
        Task<bool> HasOrdersToRequestAsync(string userId);
        Task<decimal> ReadyForRequstAsync(string userId);
        Task<decimal> RequsetedAmountAsync(string userId);
        Task<IEnumerable<PaymentListViewModel>> AllPaymentsFiltered(Expression<Func<Payment, bool>> predicate);
    }
}
