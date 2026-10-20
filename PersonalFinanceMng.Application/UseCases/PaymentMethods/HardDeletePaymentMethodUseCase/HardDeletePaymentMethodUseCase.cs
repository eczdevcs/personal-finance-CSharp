using PersonalFinanceMng.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.HardDeletePaymentMethodUseCase
{
    public class HardDeletePaymentMethodUseCase: IHardDeletePaymentMethod
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IUserRepository _userRepository;

        public HardDeletePaymentMethodUseCase(
            IPaymentMethodRepository paymentMethodRepository,
            IUserRepository userRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _userRepository = userRepository;
        }


        public async Task ExecuteAsync(Guid userId, int paymentMethodId)
        {
            // checar se o usuario tem o metodo cadastrado
            var pmFromDb = await _paymentMethodRepository.GetByIdAsync(userId, paymentMethodId)
                ?? throw new KeyNotFoundException("Método de pagamento inexistente para este usuário");

            if(pmFromDb.UserId == null)
            {
                throw new Exception("Usuário não é autorizado a deletar métodos de pagamento padrão do sistema");
            }

            await _paymentMethodRepository.HardDeleteAsync(userId, pmFromDb);
        }
    }
}
