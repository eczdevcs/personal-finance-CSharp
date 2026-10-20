using Moq;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.FindAllByUserIdUseCse;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.FindPaymentMethodByIdUseCase;
using PersonalFinanceMng.Domain.Entities;
using PersonalFinanceMng.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceMng.Tests.Unit.Application.PaymentMethodApplicationTests
{

    public class FindByIdUseCaseTest
    {

        [Fact]
        public async Task Should_returnPaymentMethodCorrectlyWhenItExistsAndBelongsToUserId()
        {
            //Arrange
            var paymentMethodRepoMock = new Mock<IPaymentMethodRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            var user = new User("Erico", "contact@ecdev.dev");

            userRepoMock.Setup(repo => repo.UserExistsAsync(user.Id)).ReturnsAsync(true);

            var pm1 = new PaymentMethod(1,"Cartão de Crédito", "Nubank", user.Id, true);
        

            var fakeList = new List<PaymentMethod>();
            fakeList.Add(pm1);


            paymentMethodRepoMock.Setup(repo => repo.GetByIdAsync(
                It.Is<Guid>(userId => userId == pm1.UserId || pm1.UserId == null),
                It.Is<int>(id => id == pm1.Id),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(pm1);


            var useCase = new FindPaymentMethodByIdUseCase( paymentMethodRepoMock.Object, userRepoMock.Object);
            // Act

            var result = await useCase.ExecuteAsync(user.Id, 1);

            // Assert

            Assert.NotNull(result);
            Assert.Equal("Cartão de Crédito",result.Name);
            Assert.Equal(result.UserId, user.Id);
        }

        [Fact]
        public async Task Should_throwErrorIfPaymentMethodWasNotFound()
        {
            //Arrange
            var paymentMethodRepoMock = new Mock<IPaymentMethodRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            var user = new User("Erico", "contact@ecdev.dev");

            userRepoMock.Setup(repo => repo.UserExistsAsync(user.Id)).ReturnsAsync(true);

            var pm1 = new PaymentMethod(1, "Cartão de Crédito", "Nubank", user.Id, true);


            paymentMethodRepoMock.Setup(repo => repo.GetByIdAsync(
                It.Is<Guid>(userId => userId == pm1.UserId || pm1.UserId == null),
                It.Is<int>(id => id == pm1.Id),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(pm1);


            var useCase = new FindPaymentMethodByIdUseCase(paymentMethodRepoMock.Object, userRepoMock.Object);
            // Act

           //Assert

            //Payment method does not exist
           await Assert.ThrowsAsync<KeyNotFoundException>(() => useCase.ExecuteAsync(user.Id, 2));
           //user does not exist
           await Assert.ThrowsAsync<KeyNotFoundException>(() => useCase.ExecuteAsync(Guid.NewGuid(), 1));
        }
    }
}
