using PersonalFinanceMng.Application.DTOs.Request;
using PersonalFinanceMng.Application.DTOs.Response;
using PersonalFinanceMng.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.UpdatePaymentMethodUseCase.cs
{
    public interface IUpdatePaymentMethodUseCase
    {
        Task<PaymentMethodResponseDTO> ExecuteAsync(
            Guid userId,
            PaymentMethodRequestDTO paymentMethod
        );
    }
}
