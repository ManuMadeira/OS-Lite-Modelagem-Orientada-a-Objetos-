using OSLite.Domain.Enums;
using OSLite.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace OSLite.Domain.Entities;

public class OrdemDeService
{
    private readonly List<ItemDeService> _itens = new();
    private Cliente? _cliente;

    public int Id { get; }
    public int ClienteId { get; private set; }
    public Cliente Cliente 
    { 
        get => _cliente ?? throw new InvalidOperationException("Cliente não definido");
        private set => _cliente = value;
    }
    public DateOnly DataAbertura { get; }
    public StatusOS Status { get; private set; }
    public ReadOnlyCollection<ItemDeService> Itens => _itens.AsReadOnly();
    public Money Total => CalcularTotal();

    // Construtor privado corrigido
    private OrdemDeService(int id, Cliente cliente, DateOnly dataAbertura)
    {
        Id = id;
        DataAbertura = dataAbertura;
        Status = StatusOS.Aberta;
        VincularCliente(cliente);
    }

    public static OrdemDeService Criar(int id, Cliente cliente, DateOnly dataAbertura)
    {
        return new OrdemDeService(id, cliente, dataAbertura);
    }

    private void VincularCliente(Cliente cliente)
    {
        Cliente = cliente;
        ClienteId = cliente.Id;
        cliente.AdicionarOrdem(this);
    }

    public void AdicionarItem(ItemDeService item)
    {
        if (Status == StatusOS.Concluida || Status == StatusOS.Cancelada)
            throw new InvalidOperationException($"Não é possível adicionar itens em OS {Status}");

        _itens.Add(item);
    }

    public void RemoverItem(ItemDeService item)
    {
        if (Status == StatusOS.Concluida || Status == StatusOS.Cancelada)
            throw new InvalidOperationException($"Não é possível remover itens em OS {Status}");

        _itens.Remove(item);
    }

    public void IniciarExecucao()
    {
        if (Status != StatusOS.Aberta)
            throw new InvalidOperationException("Só é possível iniciar execução de OS Aberta");

        if (_itens.Count == 0)
            throw new InvalidOperationException("Não é possível iniciar execução sem itens");

        Status = StatusOS.EmExecucao;
    }

    public void Concluir()
    {
        if (Status != StatusOS.EmExecucao)
            throw new InvalidOperationException("Só é possível concluir OS em execução");

        Status = StatusOS.Concluida;
    }

    public void Cancelar()
    {
        if (Status != StatusOS.Aberta && Status != StatusOS.EmExecucao)
            throw new InvalidOperationException("Só é possível cancelar OS Aberta ou em execução");

        Status = StatusOS.Cancelada;
    }

    public void TrocarCliente(Cliente novoCliente)
    {
        if (novoCliente == null)
            throw new ArgumentNullException(nameof(novoCliente));

        if (novoCliente.Id == ClienteId)
            return;

        var clienteAntigo = Cliente;
        clienteAntigo.RemoverOrdem(this);
        
        Cliente = novoCliente;
        ClienteId = novoCliente.Id;
        novoCliente.AdicionarOrdem(this);
    }

    private Money CalcularTotal()
    {
        return _itens.Aggregate(new Money(0), (total, item) => total + item.CalcularSubtotal());
    }
}