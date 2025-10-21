using OSLite.Domain.Entities;
using OSLite.Domain.Enums;
using OSLite.Domain.ValueObjects;
using Xunit;

namespace OSLite.Domain.Tests.Entities;

public class ItemDeServiceTests
{
    [Fact]
    public void ItemDeService_cria_valido_e_calcula_subtotal()
    {
        // Arrange
        var descricao = "Troca de tela";
        var quantidade = 2;
        var precoUnitario = new Money(150);
        var categoria = CategoriaItem.Pecas;

        // Act
        var item = new ItemDeService(descricao, quantidade, precoUnitario, categoria);
        var subtotal = item.CalcularSubtotal();

        // Assert
        Assert.Equal(descricao, item.Descricao);
        Assert.Equal(quantidade, item.Quantidade);
        Assert.Equal(precoUnitario, item.PrecoUnitario);
        Assert.Equal(categoria, item.Categoria);
        Assert.Equal(300, subtotal.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void ItemDeService_nao_cria_com_descricao_vazia(string descricao)
    {
        // Arrange
        var precoUnitario = new Money(100);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new ItemDeService(descricao, 1, precoUnitario));
    }

    [Fact]
    public void ItemDeService_nao_cria_com_descricao_nula()
    {
        // Arrange
        var precoUnitario = new Money(100);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new ItemDeService(null!, 1, precoUnitario));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ItemDeService_nao_cria_com_quantidade_invalida(int quantidade)
    {
        // Arrange
        var precoUnitario = new Money(100);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new ItemDeService("Item válido", quantidade, precoUnitario));
    }
}