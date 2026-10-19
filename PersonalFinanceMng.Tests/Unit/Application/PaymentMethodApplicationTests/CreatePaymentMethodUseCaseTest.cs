using Moq;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod;
using PersonalFinanceMng.Application.DTOs.Request;
using PersonalFinanceMng.Application.DTOs.Response;

using PersonalFinanceMng.Domain.Entities;
using PersonalFinanceMng.Domain.Repositories;
using System;



namespace PersonalFinanceMng.Tests.Unit.Application.PaymentMethodApplicationTests
{
    public class CreatePaymentMethodUseCaseTest
    {
        [Fact]
        public async Task Should_Create_PaymentMethod_When_User_Exists()
        {
            //Arrange
            var paymentMethodRepoMock = new Mock<IPaymentMethodRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            var user = new User("Erico", "contact@ecdev.dev");

            userRepoMock.Setup(repo => repo.UserExistsAsync(user.Id)).ReturnsAsync(true);

            var requestDTO = new  CreatePaymentMethodRequestDTO ("Cartão de Crédito", "Nubank", user.Id, true);

            var useCase = new CreatePaymentMethodUseCase(paymentMethodRepoMock.Object,  userRepoMock.Object);


            //Act
            var result = await useCase.ExecuteAsync(requestDTO);


            //Assertions
            Assert.NotNull(result);
            Assert.Equal("Cartão de Crédito", result.Name);
            Assert.Equal(user.Id, result.UserId);
        }


        [Fact]
        public async Task Should_throw_Exception_when_user_doesnot_exists()
        {
            // Arrange
            var paymentMethodRepoMock = new Mock<IPaymentMethodRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            userRepoMock.Setup(repo => repo.UserExistsAsync(Guid.NewGuid())).ReturnsAsync(false);

            var useCase = new CreatePaymentMethodUseCase(paymentMethodRepoMock.Object, userRepoMock.Object);

            var requestDTO = new CreatePaymentMethodRequestDTO( "Cartão de Crédito", "Nubank", Guid.NewGuid(), true);

            // act & assert

            await Assert.ThrowsAsync<KeyNotFoundException>( () => useCase.ExecuteAsync(requestDTO) );
            // checando se o repositorio nao foi chamado, o que e o esperado
            paymentMethodRepoMock.Verify(repo => repo.AddAsync(It.IsAny<PaymentMethod>()), Times.Never());
        }
    }
}
