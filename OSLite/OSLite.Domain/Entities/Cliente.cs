using OSLite.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace OSLite.Domain.Entities;

public class Cliente
{
    private readonly List<OrdemDeService> _ordens = new();

    public int Id { get; }
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public string Telefone { get; private set; }
    public ReadOnlyCollection<OrdemDeService> Ordens => _ordens.AsReadOnly();

    public Cliente(int id, string nome, Email email, string telefone)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio", nameof(nome));

        if (string.IsNullOrWhiteSpace(telefone))
            throw new ArgumentException("Telefone não pode ser vazio", nameof(telefone));

        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }

    internal void AdicionarOrdem(OrdemDeService ordem)
    {
        if (ordem == null)
            throw new ArgumentNullException(nameof(ordem));

        if (!_ordens.Contains(ordem))
            _ordens.Add(ordem);
    }

    internal void RemoverOrdem(OrdemDeService ordem)
    {
        if (ordem == null)
            throw new ArgumentNullException(nameof(ordem));

        _ordens.Remove(ordem);
    }
}