using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Moq;
using PersonalFinanceMng.Application.DTOs.Request;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.FindAllByUserIdUseCse;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.UpdatePaymentMethodUseCase.cs;
using PersonalFinanceMng.Domain.Entities;
using PersonalFinanceMng.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Tests.Unit.Application.PaymentMethodApplicationTests
{
    public class UpdatePaymentMethodUseCaseTest
    {
        [Fact]
        public async Task Should_UpdatePaymentMethodCorrectly()
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


            paymentMethodRepoMock.Setup(repo => repo.GetByIdAsync(
                user.Id,
                pm1.Id, 
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(pm1);


            paymentMethodRepoMock.Setup(repo => repo.UpdateAsync(
            It.IsAny<Guid>(),
            It.IsAny<PaymentMethod>(),
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(
                (
                    Guid uId, 
                    PaymentMethod pm, 
                    CancellationToken ct
                ) => pm
             );

            var pm2 = new PaymentMethod(
                1,
                "VR",
                "IQVIA",
                user.Id,
                true
            );

            var useCase = new UpdatePaymentMethodUseCase(
                paymentMethodRepoMock.Object,
                userRepoMock.Object
            );

            //Act
            var updated = await useCase.ExecuteAsync(user.Id, pm2);
         

            //Assertions
            Assert.NotNull(updated);
            Assert.Equal(1, updated.Id);
            Assert.Equal("VR", updated.Name);
            Assert.Equal("IQVIA", updated.Description);
            paymentMethodRepoMock.Verify(
                repo => repo.UpdateAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<PaymentMethod>(), 
                    It.IsAny<CancellationToken>()
                ),
                Times.Once()
            );
        }

        [Fact]
        public async Task Should_ThrowErrorEitherIfMethodIdOrMethodUserIdDoesNotMatch()
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


            paymentMethodRepoMock.Setup(repo => repo.GetByIdAsync(
                user.Id,
                pm1.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(pm1);


            paymentMethodRepoMock.Setup(repo => repo.UpdateAsync(
            It.IsAny<Guid>(),
            It.IsAny<PaymentMethod>(),
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(
                (
                    Guid uId,
                    PaymentMethod pm,
                    CancellationToken ct
                ) => pm
             );

            var pm2 = new PaymentMethod(
                1,
                "VR",
                "IQVIA",
                user.Id,
                true
            );

            var useCase = new UpdatePaymentMethodUseCase(
                paymentMethodRepoMock.Object,
                userRepoMock.Object
            );

            //Act
            var updated = await useCase.ExecuteAsync(user.Id, pm2);


            //Assertions

            //user id mismatch
            var newGuid = Guid.NewGuid();
            var userIdException = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => useCase.ExecuteAsync(newGuid, pm2)
            );

            Assert.Equal($"Usuário id {newGuid} inexistente ou   não possui este método cadastrado.", userIdException.Message);

            // if method.userId is null it is a default method and can not be changed
            var pmDefault = new PaymentMethod("Dinheiro", "Dinheiro vivo cédulas");
            var defaultMethodException =  await Assert.ThrowsAsync<KeyNotFoundException>(
               () => useCase.ExecuteAsync(user.Id, pmDefault)
            );

            Assert.Equal($"Usuário id {user.Id} inexistente ou   não possui este método cadastrado.", defaultMethodException.Message);
            paymentMethodRepoMock.Verify(
                repo => repo.UpdateAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<PaymentMethod>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once()
            );
        }

       
    }
}
