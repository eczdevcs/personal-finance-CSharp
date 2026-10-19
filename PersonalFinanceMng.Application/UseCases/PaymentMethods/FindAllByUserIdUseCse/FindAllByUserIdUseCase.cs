using System;
using System.Collections.Generic;

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

        public async Task<IEnumerable<PaymentMethod>> ExecuteAsync(Guid userId)
        {
            var userExists = await _userRepository.UserExistsAsync( userId );

            if (userExists == false)
            {
                throw new KeyNotFoundException($"Usuário id {userId} not found.");
            }

            return await _paymentMethodRepository.GetAllByUserIdAsync( userId );
        }
    }
}
