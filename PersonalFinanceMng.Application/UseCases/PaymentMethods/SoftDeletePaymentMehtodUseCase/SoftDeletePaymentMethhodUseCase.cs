using PersonalFinanceMng.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PersonalFinanceMng.Application.UseCases.PaymentMethods.SoftDeletePaymentMehtodUseCase
{
    public class SoftDeletePaymentMethodUseCase : ISoftDeletePaymentMethodUseCase
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IUserRepository _userRepository;

        public SoftDeletePaymentMethodUseCase(
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

            // O metodo e encontrado se for do id do usuario ou nulo,
            // neste ultimo caso e do sistema e nao pode ser deletado
            if (pmFromDb.UserId == null)
            {
                throw new Exception("Usuário não é autorizado a deletar métodos de pagamento padrão do sistema");
            }

            await _paymentMethodRepository.SoftDeleteAsync(userId, pmFromDb);
        }
    }
}
