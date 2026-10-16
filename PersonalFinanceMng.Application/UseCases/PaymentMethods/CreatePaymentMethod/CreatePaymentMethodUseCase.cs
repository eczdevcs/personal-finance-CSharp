using PersonalFinanceMng.Domain.Repositories;
using PersonalFinanceMng.Domain.Entities;
using System;

using PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod;
namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod
{
    class CreatePaymentMethodUseCase : ICreatePaymentMethodUseCase

    {
        // posteriormente injetar um repositorio de usuario, para checagem previa da existencia dele 
        private readonly IPaymentMethodRepository _paymentMethodRepsitory;

        public CreatePaymentMethodUseCase(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepsitory = paymentMethodRepository;
        }

        public async Task<PaymentMethodResponseDTO> 
            ExcecuteAsync(CreatePaymentMethodRequestDTO requestDTO)
        {
           var paymentMethod = new PaymentMethod(requestDTO.Name, requestDTO.Description, requestDTO.UserId, true);

            // O correto seria checar se o usuario existe, para proceder ao registro
            // Para checagem um user repository teria que ser injetado para buscar e checar
 
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
