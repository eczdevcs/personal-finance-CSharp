using PersonalFinanceMng.Application.DTOs.Response;
using PersonalFinanceMng.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.FindAllByUserIdUseCse
{
   public interface IFindAllByUserIdUseCase
   {
        Task<IEnumerable<PaymentMethodResponseDTO>> ExecuteAsync(Guid userId);
   }
}
