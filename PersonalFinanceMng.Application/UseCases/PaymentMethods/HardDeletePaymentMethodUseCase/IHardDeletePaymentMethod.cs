using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.HardDeletePaymentMethodUseCase
{
    public interface IHardDeletePaymentMethod
    {
        public interface ISoftDeletePaymentMethodUseCase
        {
            public Task ExecuteAsync(Guid userId, int paymentMethodId);
        }
    }
}
