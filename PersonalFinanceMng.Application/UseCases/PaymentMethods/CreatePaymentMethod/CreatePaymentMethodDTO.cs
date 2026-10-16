using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod
{
    public record CreatePaymentMethodRequestDTO
    (
        string Name,
        string Description,
        int UserId,
        bool IsActive
    );
}
