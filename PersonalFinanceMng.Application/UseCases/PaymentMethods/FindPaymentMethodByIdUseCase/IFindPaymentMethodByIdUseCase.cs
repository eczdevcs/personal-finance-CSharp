using PersonalFinanceMng.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.FindPaymentMethodByIdUseCase
{
     public interface IFindPaymentMethodByIdUseCase
    {
        Task<PaymentMethodResponseDTO> ExecuteAsync
        (
            Guid userId,
            int methodId
        );
     }
}



