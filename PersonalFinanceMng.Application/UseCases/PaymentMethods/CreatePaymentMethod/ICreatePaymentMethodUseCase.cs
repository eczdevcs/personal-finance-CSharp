using PersonalFinanceMng.Application.DTOs.Request;
using PersonalFinanceMng.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod
{
    public interface ICreatePaymentMethodUseCase
    {
        Task<PaymentMethodResponseDTO> ExecuteAsync
        (
            CreatePaymentMethodRequestDTO  requestDTO
        );
    }
}
