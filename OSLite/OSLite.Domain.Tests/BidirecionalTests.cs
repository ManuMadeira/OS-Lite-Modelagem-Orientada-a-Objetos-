using OSLite.Domain.Entities;
using OSLite.Domain.ValueObjects;
using Xunit;

namespace OSLite.Domain.Tests;

public class BidirecionalTests
{
    [Fact]
    public void Cliente_adiciona_ordem_sincroniza_cliente_na_ordem()
    {
        // Arrange
        var cliente = new Cliente(1, "Maria", new Email("maria@email.com"), "11888888888");
        var ordem = OrdemDeService.Criar(1, cliente, DateOnly.FromDateTime(DateTime.Now));

        // Assert
        Assert.Single(cliente.Ordens);
        Assert.Contains(ordem, cliente.Ordens);
        Assert.Equal(cliente.Id, ordem.ClienteId);
        Assert.Equal(cliente, ordem.Cliente);
    }

    [Fact]
    public void OS_trocar_de_cliente_atualiza_colecoes_dos_clientes()
    {
        // Arrange
        var cliente1 = new Cliente(1, "Cliente 1", new Email("cliente1@email.com"), "11111111111");
        var cliente2 = new Cliente(2, "Cliente 2", new Email("cliente2@email.com"), "22222222222");
        
        var ordem = OrdemDeService.Criar(1, cliente1, DateOnly.FromDateTime(DateTime.Now));

        // Act
        ordem.TrocarCliente(cliente2);

        // Assert
        Assert.DoesNotContain(ordem, cliente1.Ordens);
        Assert.Contains(ordem, cliente2.Ordens);
        Assert.Equal(cliente2.Id, ordem.ClienteId);
        Assert.Equal(cliente2, ordem.Cliente);
    }
}