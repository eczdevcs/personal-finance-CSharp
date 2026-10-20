using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.DTOs.Response
{
    public record PaymentMethodResponseDTO
    (
        int Id, 
        string Name,
        Guid? UserId,
        string Description,
        bool IsActive,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
