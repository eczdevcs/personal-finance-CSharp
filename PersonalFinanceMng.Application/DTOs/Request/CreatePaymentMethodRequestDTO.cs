using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.DTOs.Request
{ 
    public record CreatePaymentMethodRequestDTO
    (
        string Name,
        string Description,
        Guid UserId,
        bool IsActive
    );
}
