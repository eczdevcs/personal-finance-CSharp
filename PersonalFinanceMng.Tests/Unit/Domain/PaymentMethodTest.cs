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
        var pm1 = new PaymentMethod(1, "Pix", null, null, true);
        var pm2 = new PaymentMethod(1, "Dinheiro", null, null, true);

        var result = pm1.Equals(pm2);

        result.Should().BeTrue();
    }
}