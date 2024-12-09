using EldenBoost.Core.Models.Payment;
using EldenBoost.Infrastructure.Data.Models;
using System.Linq.Expressions;

namespace EldenBoost.Core.Contracts
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(string userId);
        Task<bool> IsPendingAsync(string userId);
        Task<bool> HasOrdersToRequestAsync(string userId);
        Task<decimal> ReadyForRequstAsync(string userId);
        Task<decimal> RequestedAmountAsync(string userId);
        Task<IEnumerable<PaymentListViewModel>> AllPaymentsFiltered(Expression<Func<Payment, bool>> predicate);
        Task PayAsync(int paymentId);
        Task<bool> ExistsByIdAsync(int paymentId);
        Task<PaymentCountDataModel> GetPaymentCountDataAsync();
    }
}
