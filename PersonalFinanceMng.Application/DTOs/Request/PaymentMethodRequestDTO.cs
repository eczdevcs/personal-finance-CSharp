using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PersonalFinanceMng.Application.DTOs.Request
{
    public record PaymentMethodRequestDTO
    (
        int Id,
        string Name,
        string Description,
        Guid UserId,
        bool IsActive
    );
}
