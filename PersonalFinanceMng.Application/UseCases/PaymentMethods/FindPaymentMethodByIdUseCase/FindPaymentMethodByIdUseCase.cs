using PersonalFinanceMng.Application.DTOs.Response;
using PersonalFinanceMng.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.FindPaymentMethodByIdUseCase
{
    public class FindPaymentMethodByIdUseCase : IFindPaymentMethodByIdUseCase
    {

        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IUserRepository _userRepository;
        public FindPaymentMethodByIdUseCase(IPaymentMethodRepository paymentMethodRespository, IUserRepository userRepository)
        {
            _paymentMethodRepository = paymentMethodRespository;
            _userRepository = userRepository;
        }

        async public Task<PaymentMethodResponseDTO> ExecuteAsync(Guid userId, int methodId)
        {

            var userExists = await _userRepository.UserExistsAsync(userId);

            if(userExists == false)
            {
                 throw new KeyNotFoundException($"Usuário id {userId} not found.");
            }

            var result = await _paymentMethodRepository.GetByIdAsync(userId, methodId);

            return result == null
                ? throw new KeyNotFoundException($"Método de pagamento com o id {methodId} não encontrado para este usuário.")
                : new PaymentMethodResponseDTO
            (
                result.Id,
                result.Name,
                result.UserId,
                result.Description,
                result.IsActive,
                result.CreatedAt,
                result.UpdatedAt
            );
        }
    }
}
