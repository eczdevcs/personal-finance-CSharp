using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod
{
    public interface ICreatePaymentMethodUseCase
    {
        Task<PaymentMethodResponseDTO> ExcecuteAsync(
            CreatePaymentMethodRequestDTO  requestDTO
        );
    }
}
