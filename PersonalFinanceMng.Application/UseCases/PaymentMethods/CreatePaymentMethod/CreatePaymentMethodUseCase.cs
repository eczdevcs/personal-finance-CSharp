using PersonalFinanceMng.Domain.Repositories;
using PersonalFinanceMng.Domain.Entities;
using System;

using PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod;
namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod
{
    public class CreatePaymentMethodUseCase : ICreatePaymentMethodUseCase

    {
     
        private readonly IPaymentMethodRepository _paymentMethodRepsitory;
        private readonly IUserRepository _userRepository;


        public CreatePaymentMethodUseCase(IPaymentMethodRepository paymentMethodRepository, IUserRepository userRepository)
        {
            _paymentMethodRepsitory = paymentMethodRepository;
            _userRepository = userRepository;
        }




        public async Task<PaymentMethodResponseDTO> 
            ExecuteAsync(CreatePaymentMethodRequestDTO requestDTO)
        {
          
            var userExists = await _userRepository.UserExistsAsync(requestDTO.UserId);

            if (userExists == false) 
            {
                throw new KeyNotFoundException($"Usuário com ID {requestDTO.UserId} não foi encontrado.");
            }

            var paymentMethod = new PaymentMethod(requestDTO.Name, requestDTO.Description, requestDTO.UserId, true);

            await _paymentMethodRepsitory.AddAsync(paymentMethod);

            return new PaymentMethodResponseDTO (
                paymentMethod.Id, 
                paymentMethod.Name, 
                paymentMethod.UserId, 
                paymentMethod.IsActive,
                paymentMethod.CreatedAt,
                paymentMethod.UpdatedAt
            );
        }
    }
}
