using System;
using FluentAssertions;
using PersonalFinanceMng.Domain.Entities;
using Xunit;

namespace PersonalFinanceMng.Tests.Unit.Domain;

public class PaymentMethodTests
{
    [Theory]
    [InlineData("  Cartão   de   Crédito  ", "Cartão   de   Crédito", "cartão de crédito")]
    [InlineData("  PIX  ", "PIX", "pix")]
    [InlineData("Dinheiro", "Dinheiro", "dinheiro")]
    public void SetName_ShouldFormatNameAndNormalizedNameCorrectly(
        string input,
        string expectedName,
        string expetedNormalizedName)
    {
        var userId = Guid.NewGuid();
        var paymentMethod = new PaymentMethod(1, input, null, userId);

        paymentMethod.Name.Should().Be(expectedName);
        paymentMethod.NormalizedName.Should().Be(expetedNormalizedName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SetName_WithInvalidName_ShouldThrowAnException(string invalidName)
    {
        //arrange
        var userId = Guid.NewGuid();

        //assertion
        FluentActions.Invoking(() => new PaymentMethod(id:1, name: invalidName, description:  null, userId,isActive: true))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("*O Nome do método deve ser fornecido: *");
    }

    [Fact]
    public void Equals_With_SameId_Should_return_true()
    {
        var userId = Guid.NewGuid();
        var pm1 = new PaymentMethod(1, "Pix", null, userId, true);
        var pm2 = new PaymentMethod(1, "Dinheiro", null, userId, true);

        var result = pm1.Equals(pm2);

        result.Should().BeTrue();
    }

    [Fact]
    public void SetDetails_WithOrWithoutNameAndDescriptionNecessarelyProvided_ShouldSetCorrectly()
    {
        var userId = Guid.NewGuid();
        //Arrange
        var pm = new PaymentMethod("Pix", "Pagamento isntantaneo", userId);
        var pm2 = new PaymentMethod("VR", "Vale", userId);
        // Act
        // providing both name and description
        pm.UpdateDetails("Dinheiro", "Dinheiro vivo");
        //providing just description
        pm2.UpdateDetails(null, "Vale refeicao Flash");


        // Assert
        pm.Name.Should().Be("Dinheiro");
        pm.Description.Should().Be("Dinheiro vivo");
        pm2.Name.Should().Be("VR");
        pm2.Description.Should().Be("Vale refeicao Flash");
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        //Arrange

        var userId = Guid.NewGuid();
            var pm = new PaymentMethod("Pix", "Pagamento isntantaneo", userId, true);

        //Act
             pm.Deactivate();
        //Assert
        pm.Should().NotBe(null);
        pm.Name.Should().Be("Pix");
        pm.Description.Should().Be("Pagamento isntantaneo");
        pm.IsActive.Should().Be(false);
    }

    [Fact]
    public void Equals_ShouldReturTrueIfIdAreEqualFalseNoProvidedIds()
    {
        var userId = Guid.NewGuid();

        //Arrange
        var pm1 = new PaymentMethod(1, "Pix", "Pagamento isntantaneo", userId, true);
        var pm2 = new PaymentMethod(1, "Pix", "Pagamento isntantaneo", userId, true);

        var pm3WithoutId =  new  PaymentMethod("Pix", "Pagamento isntantaneo", userId, true);
        var pm4WithoutId = new PaymentMethod("Pix", "Pagamento isntantaneo", userId, true);

        // Act
        var resultTwoIdEquals = pm1.Equals(pm2);
        var resultNoIdProvided = pm3WithoutId.Equals(pm4WithoutId);


        // Assertion 

        resultTwoIdEquals.Should().BeTrue();
        resultNoIdProvided.Should().BeFalse();
    }

}