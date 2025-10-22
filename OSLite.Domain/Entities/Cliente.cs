using System;
using System.Collections.Generic;
using System.Linq;

namespace OSLite.Domain.Entities
{
    public sealed class Cliente
    {
        private readonly List<OrdemDeServico> _ordens = new();
        public int Id { get; }
        public string Nome { get; private set; }
        public string? Telefone { get; private set; }
        public IReadOnlyCollection<OrdemDeServico> Ordens => _ordens.AsReadOnly();

        public Cliente(int id, string nome, string? telefone = null)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome cannot be empty.", nameof(nome));
            Id = id;
            Nome = nome;
            Telefone = telefone;
        }

        internal void AddOrdemInternal(OrdemDeServico ordem)
        {
            if (!_ordens.Contains(ordem)) _ordens.Add(ordem);
        }

        internal void RemoveOrdemInternal(OrdemDeServico ordem)
        {
            _ordens.Remove(ordem);
        }

        public OrdemDeServico OpenOrdem(int ordemId)
        {
            var ordem = OrdemDeServico.Create(ordemId, this);
            AddOrdemInternal(ordem);
            return ordem;
        }
    }
}
