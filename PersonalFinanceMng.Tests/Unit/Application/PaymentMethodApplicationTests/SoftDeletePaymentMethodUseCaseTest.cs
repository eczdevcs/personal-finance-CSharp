using Moq;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.SoftDeletePaymentMehtodUseCase;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.UpdatePaymentMethodUseCase.cs;
using PersonalFinanceMng.Domain.Entities;
using PersonalFinanceMng.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Tests.Unit.Application.PaymentMethodApplicationTests
{
    
    public  class SoftDeletePaymentMethodUseCaseTest
    {
        [Fact]
        public async Task Should_SoftDeletePaymentMethod()
        {
      
            //Arrange
            var paymentMethodRepoMock = new Mock<IPaymentMethodRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            var user = new User("Erico", "contact@ecdev.dev");

            var pm1 = new PaymentMethod(
                1,
                "Cartão de Crédito",
                "Nubank",
                user.Id,
                true
            );

            userRepoMock.Setup(repo => repo
            .UserExistsAsync(user.Id))
            .ReturnsAsync(true);


            paymentMethodRepoMock.Setup(
                repo => repo.GetByIdAsync(
                    user.Id,
                    pm1.Id,
                    It.IsAny<CancellationToken>()    
                )
            ).ReturnsAsync(pm1);


            var useCase = new SoftDeletePaymentMethodUseCase(
                paymentMethodRepoMock.Object,
                userRepoMock.Object
            );

            //Act
           await useCase.ExecuteAsync(user.Id, pm1.Id);


            //Assertions
            paymentMethodRepoMock.Verify(repo => repo.SoftDeleteAsync(
                user.Id,
                It.Is<PaymentMethod>(pm => pm.Id == pm1.Id), 
                It.IsAny<CancellationToken>()
            ),
            Times.Once()
            );
        }

        [Fact]
        public async Task Should_ThrowErrorIfUserOrPaymentMehtodIdMismatch()
        {
            //Arrange
            var paymentMethodRepoMock = new Mock<IPaymentMethodRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            var user = new User("Erico", "contact@ecdev.dev");

            var pm = new PaymentMethod(
                "Cartão de Crédito",
                "Nubank"
            );

            userRepoMock.Setup(repo => repo
            .UserExistsAsync(user.Id))
            .ReturnsAsync(true);


            paymentMethodRepoMock.Setup(
                repo => repo.GetByIdAsync(
                    user.Id,
                    1,
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(pm);


            var useCase = new SoftDeletePaymentMethodUseCase(
                paymentMethodRepoMock.Object,
                userRepoMock.Object
            );

            //Act
        
            // trying to delete default method
            var defaultDeletionError =  await Assert.ThrowsAsync<Exception>(()=> useCase.ExecuteAsync(user.Id, 1));
            Assert.Equal("Usuário não é autorizado a deletar métodos de pagamento padrão do sistema", defaultDeletionError.Message);

            var inexistingMethod = await Assert.ThrowsAsync<KeyNotFoundException>(() => useCase.ExecuteAsync(user.Id, 60));
            Assert.Equal("Método de pagamento inexistente para este usuário", inexistingMethod.Message);

        }
    }
}
