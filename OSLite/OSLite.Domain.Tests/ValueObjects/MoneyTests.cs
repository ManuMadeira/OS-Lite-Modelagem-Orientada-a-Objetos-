using OSLite.Domain.ValueObjects;
using Xunit;

namespace OSLite.Domain.Tests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Money_nao_aceita_negativo()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Money(-1));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10.5)]
    [InlineData(1000)]
    public void Money_cria_valido_com_valor_nao_negativo(decimal valor)
    {
        // Act
        var money = new Money(valor);

        // Assert
        Assert.Equal(valor, money.Value);
    }

    [Fact]
    public void Money_soma_corretamente()
    {
        // Arrange
        var money1 = new Money(10);
        var money2 = new Money(20);

        // Act
        var resultado = money1 + money2;

        // Assert
        Assert.Equal(30, resultado.Value);
    }

    [Fact]
    public void Money_multiplica_por_quantidade_corretamente()
    {
        // Arrange
        var money = new Money(15);
        var quantidade = 3;

        // Act
        var resultado = money * quantidade;

        // Assert
        Assert.Equal(45, resultado.Value);
    }
}