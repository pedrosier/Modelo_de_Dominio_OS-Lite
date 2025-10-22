using System;
using OSLite.Domain.ValueObjects;

namespace OSLite.Domain.Entities
{
    public sealed class ItemDeServico
    {
        public string Descricao { get; }
        public int Quantidade { get; private set; }
        public Money PrecoUnitario { get; }
        public CategoriaItem? Categoria { get; }

        public ItemDeServico(string descricao, int quantidade, Money precoUnitario, CategoriaItem? categoria = null)
        {
            if (string.IsNullOrWhiteSpace(descricao)) throw new ArgumentException("Descricao cannot be empty.", nameof(descricao));
            if (quantidade <= 0) throw new ArgumentOutOfRangeException(nameof(quantidade), "Quantidade must be greater than zero.");
            Descricao = descricao;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            Categoria = categoria;
        }

        public decimal Subtotal() => PrecoUnitario.Multiply(Quantidade);
    }
}
