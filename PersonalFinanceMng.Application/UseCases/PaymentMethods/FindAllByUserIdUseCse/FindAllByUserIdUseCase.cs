using System;
using System.Collections.Generic;
using PersonalFinanceMng.Application.DTOs.Response;
using PersonalFinanceMng.Domain.Entities;
using PersonalFinanceMng.Domain.Repositories;


namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.FindAllByUserIdUseCse
{
    public class FindAllByUserIdUseCase : IFindAllByUserIdUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public FindAllByUserIdUseCase(
            IUserRepository userRepository, 
            IPaymentMethodRepository paymentMethodRepository
        ) 
        {
            _userRepository = userRepository;
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<IEnumerable<PaymentMethodResponseDTO>> ExecuteAsync(Guid userId)
        {
            var userExists = await _userRepository.UserExistsAsync( userId );

            if (userExists == false)
            {
                throw new KeyNotFoundException($"Usuário id {userId} not found.");
            }

            var list = await _paymentMethodRepository.GetAllByUserIdAsync( userId );

            IEnumerable<PaymentMethodResponseDTO> DTOList = list.Select(
                pm => new PaymentMethodResponseDTO(
                   pm.Id,
                   pm.Name,
                   pm.UserId,
                   pm.Description,
                   pm.IsActive,
                   pm.CreatedAt,
                   pm.UpdatedAt
                )
            ).ToList();

            return DTOList;
        }
    }
}
