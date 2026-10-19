using Microsoft.VisualStudio.TestPlatform.Common;
using Moq;
using PersonalFinanceMng.Application.DTOs.Request;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.FindAllByUserIdUseCse;
using PersonalFinanceMng.Domain.Entities;
using PersonalFinanceMng.Domain.Repositories;


namespace PersonalFinanceMng.Tests.Unit.Application.PaymentMethodApplicationTests
{
   public class FindAllByIdUseCaseTest
    {
        [Fact]
        public async Task Should_FindAllPaymentMethodsRecordsForUser()
        {
            //Arrange
            var paymentMethodRepoMock = new Mock<IPaymentMethodRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            var user = new User("Erico", "contact@ecdev.dev");

            userRepoMock.Setup(repo => repo.UserExistsAsync(user.Id)).ReturnsAsync(true);

            var pm1 = new PaymentMethod("Cartão de Crédito", "Nubank", user.Id, true);
            var pm2 = new PaymentMethod("VR", "IQVIA", user.Id, true);

            var fakeList = new List<PaymentMethod>();
            fakeList.Add(pm1);
            fakeList.Add(pm2);

            // mockando comportamento do metodo
            paymentMethodRepoMock.Setup(
                repo => repo.GetAllByUserIdAsync(
                    user.Id,
                    It.IsAny<CancellationToken>())
                ).ReturnsAsync(fakeList);

            var useCase = new FindAllByUserIdUseCase(
                userRepoMock.Object,
                paymentMethodRepoMock.Object
             );
            // Act

            var result = await useCase.ExecuteAsync(user.Id);

            // Assert

            var firstItem = result.First();
            Assert.Equal("Cartão de Crédito", firstItem.Name);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

        }

        [Fact]
        public async Task Should_throwAnErrorIfUserDoesNotExists()
        {
            //Arrange
            var paymentMethodRepoMock = new Mock<IPaymentMethodRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            var user = new User("Erico", "contact@ecdev.dev");

            userRepoMock.Setup(repo => repo.UserExistsAsync(user.Id)).ReturnsAsync(false);

            var pm1 = new PaymentMethod("Cartão de Crédito", "Nubank", user.Id, true);
            var pm2 = new PaymentMethod("VR", "IQVIA", user.Id, true);

            var fakeList = new List<PaymentMethod>();
            fakeList.Add(pm1);
            fakeList.Add(pm2);

            // mockando comportamento do metodo
            paymentMethodRepoMock.Setup(repo => repo.GetAllByUserIdAsync(
                user.Id, 
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(fakeList);

            var useCase = new FindAllByUserIdUseCase(
                userRepoMock.Object, 
                paymentMethodRepoMock.Object
            );

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => useCase.ExecuteAsync(user.Id)
             );

            //// checando se o repositorio nao foi chamado, o que e o esperado
            paymentMethodRepoMock.Verify(
                repo => repo.GetAllByUserIdAsync(
                    user.Id, 
                    It.IsAny<CancellationToken>()), 
                    Times.Never()
            );
        }

   }
}
