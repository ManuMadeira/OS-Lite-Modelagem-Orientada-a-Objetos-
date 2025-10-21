using OSLite.Domain.Entities;
using OSLite.Domain.Enums;
using OSLite.Domain.ValueObjects;
using Xunit;

namespace OSLite.Domain.Tests.Entities;

public class OrdemDeServiceTests
{
    private readonly Cliente _cliente;
    private readonly OrdemDeService _ordem;

    public OrdemDeServiceTests()
    {
        _cliente = new Cliente(1, "João Silva", new Email("joao@email.com"), "11999999999");
        _ordem = OrdemDeService.Criar(1, _cliente, DateOnly.FromDateTime(DateTime.Now));
    }

    [Fact]
    public void OS_total_soma_subtotais_itens()
    {
        // Arrange
        var item1 = new ItemDeService("Item 1", 2, new Money(100));
        var item2 = new ItemDeService("Item 2", 1, new Money(50));

        // Act
        _ordem.AdicionarItem(item1);
        _ordem.AdicionarItem(item2);

        // Assert
        Assert.Equal(250, _ordem.Total.Value);
    }

    [Fact]
    public void OS_aberta_inicia_execucao_quando_tem_itens()
    {
        // Arrange
        var item = new ItemDeService("Item teste", 1, new Money(100));
        _ordem.AdicionarItem(item);

        // Act
        _ordem.IniciarExecucao();

        // Assert
        Assert.Equal(StatusOS.EmExecucao, _ordem.Status);
    }

    [Fact]
    public void OS_aberta_nao_inicia_sem_itens()
    {
        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _ordem.IniciarExecucao());
        Assert.Equal("Não é possível iniciar execução sem itens", exception.Message);
    }

    [Fact]
    public void OS_nao_adiciona_itens_em_concluida()
    {
        // Arrange
        var item1 = new ItemDeService("Item 1", 1, new Money(100));
        _ordem.AdicionarItem(item1);
        _ordem.IniciarExecucao();
        _ordem.Concluir();

        var item2 = new ItemDeService("Item 2", 1, new Money(50));

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _ordem.AdicionarItem(item2));
        Assert.Equal("Não é possível adicionar itens em OS Concluida", exception.Message);
    }

    [Fact]
    public void OS_fluxo_aberta_para_execucao_para_concluida()
    {
        // Arrange
        var item = new ItemDeService("Item teste", 1, new Money(100));
        _ordem.AdicionarItem(item);

        // Act
        _ordem.IniciarExecucao();
        _ordem.Concluir();

        // Assert
        Assert.Equal(StatusOS.Concluida, _ordem.Status);
    }
}