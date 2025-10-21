using OSLite.Domain.Enums;
using OSLite.Domain.ValueObjects;

namespace OSLite.Domain.Entities;

public class ItemDeService
{
    public string Descricao { get; }
    public int Quantidade { get; }
    public Money PrecoUnitario { get; }
    public CategoriaItem? Categoria { get; }

    public ItemDeService(string descricao, int quantidade, Money precoUnitario, CategoriaItem? categoria = null)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição não pode ser vazia", nameof(descricao));

        if (quantidade <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantidade), "Quantidade deve ser maior que zero");

        Descricao = descricao;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        Categoria = categoria;
    }

    public Money CalcularSubtotal() => PrecoUnitario * Quantidade;
}