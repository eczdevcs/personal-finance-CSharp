using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.SoftDeletePaymentMehtodUseCase
{
    public interface ISoftDeletePaymentMethodUseCase
    {
        public Task ExecuteAsync(Guid userId, int paymentMethodId);
    }
}
