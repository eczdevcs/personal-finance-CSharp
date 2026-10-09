using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PersonalFinanceMng.Domain.Entities;

namespace PersonalFinanceMng.Domain.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<PaymentMethod?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PaymentMethod>> GetAllByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task AddAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken = default);
        void Update(PaymentMethod paymentMethod);
        void Delete(PaymentMethod paymentMethod);
    }
}