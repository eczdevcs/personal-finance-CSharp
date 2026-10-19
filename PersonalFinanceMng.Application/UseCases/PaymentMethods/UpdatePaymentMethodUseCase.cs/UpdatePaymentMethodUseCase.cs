using PersonalFinanceMng.Application.DTOs.Request;
using PersonalFinanceMng.Application.DTOs.Response;
using PersonalFinanceMng.Domain.Entities;
using PersonalFinanceMng.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.UpdatePaymentMethodUseCase.cs
{
    public class UpdatePaymentMethodUseCase : IUpdatePaymentMethodUseCase
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IUserRepository _userRepository;

        public UpdatePaymentMethodUseCase(
            IPaymentMethodRepository paymentMethodRespository, 
            IUserRepository userRepository)
        {
            _paymentMethodRepository = paymentMethodRespository;
            _userRepository = userRepository;
        }

        async public Task<PaymentMethod> ExecuteAsync(
            Guid userId, 
            PaymentMethod paymentMethod
        )
        {

            var methodFromDb = await _paymentMethodRepository.GetByIdAsync(
                userId, 
                paymentMethod.Id
            ) 
            ?? 
                throw new KeyNotFoundException(
                    $"Usuário id {userId} inexistente ou   não possui este método cadastrado."
                );

            if (methodFromDb.UserId == null)
            {
                throw new KeyNotFoundException(
                    $"O método de pagamento nao pertence ao usuário {userId}  ou não está cadastrado."
                );
            }

            methodFromDb.UpdateDetails(paymentMethod.Name, paymentMethod.Description);
            
            var updated = await _paymentMethodRepository.UpdateAsync(userId,methodFromDb);

            return updated;
        }

     
    }
}
