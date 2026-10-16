using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod
{
    public record PaymentMethodResponseDTO
    (
        int Id, 
        string Name,
        int? UserId,
        bool IsActive,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
