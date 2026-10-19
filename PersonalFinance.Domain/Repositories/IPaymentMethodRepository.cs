using System.Collections.Generic;

using PersonalFinanceMng.Domain.Entities;

namespace PersonalFinanceMng.Domain.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<PaymentMethod?> GetByIdAsync(
            Guid userId, 
            int paymentMethodId, 
            CancellationToken cancellationToken = default
        );

        Task<IEnumerable<PaymentMethod>> GetAllByUserIdAsync(
            Guid userId, 
            CancellationToken cancellationToken = default
        );

        Task AddAsync(
            PaymentMethod paymentMethod, 
            CancellationToken cancellationToken = default
        );

        Task<PaymentMethod> UpdateAsync(
            Guid userId, 
            PaymentMethod paymentMethod, 
            CancellationToken cancellationToken = default
        );

        Task SoftDeleteAsync(
            Guid userId,
            PaymentMethod paymentMethod, 
            CancellationToken cancellationToken = default
        );

        Task HardDeleteAsync(
            Guid userId, 
            PaymentMethod paymentMethodId, 
            CancellationToken cancellationToken = default
        );
    }
}