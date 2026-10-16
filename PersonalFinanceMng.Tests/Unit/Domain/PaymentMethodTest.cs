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
        var paymentMethod = new PaymentMethod(1, input, null, userId: 1);

        paymentMethod.Name.Should().Be(expectedName);
        paymentMethod.NormalizedName.Should().Be(expetedNormalizedName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SetName_WithInvalidName_ShouldThrowAnException(string invalidName)
    {
        FluentActions.Invoking(() => new PaymentMethod(id:1, name: invalidName, description:  null, userId: 1,isActive: true))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("*O Nome do método deve ser fornecido: *");
    }

    [Fact]
    public void Equals_With_SameId_Should_return_true()
    {
        var pm1 = new PaymentMethod(1, "Pix", null, 1, true);
        var pm2 = new PaymentMethod(1, "Dinheiro", null, 1, true);

        var result = pm1.Equals(pm2);

        result.Should().BeTrue();
    }

    [Fact]
    public void SetDetails_WithOrWithoutNameAndDescriptionNecessarelyProvided_ShouldSetCorrectly()
    {
        //Arrange
        var pm = new PaymentMethod("Pix", "Pagamento isntantaneo", 1);
        var pm2 = new PaymentMethod("VR", "Vale", 1);
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
            var pm = new PaymentMethod("Pix", "Pagamento isntantaneo", 1, true);

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

        //Arrange

        var pm1 = new PaymentMethod(1, "Pix", "Pagamento isntantaneo", 1, true);
        var pm2 = new PaymentMethod(1, "Pix", "Pagamento isntantaneo", 1, true);

        var pm3WithoutId =  new  PaymentMethod("Pix", "Pagamento isntantaneo", 1, true);
        var pm4WithoutId = new PaymentMethod("Pix", "Pagamento isntantaneo", 1, true);

        // Act
        var resultTwoIdEquals = pm1.Equals(pm2);
        var resultNoIdProvided = pm3WithoutId.Equals(pm4WithoutId);


        // Assertion 

        resultTwoIdEquals.Should().BeTrue();
        resultNoIdProvided.Should().BeFalse();
    }

}