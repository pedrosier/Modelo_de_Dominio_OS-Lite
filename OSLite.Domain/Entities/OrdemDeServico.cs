using System;
using System.Collections.Generic;
using System.Linq;
using OSLite.Domain.ValueObjects;

namespace OSLite.Domain.Entities
{
    public sealed class OrdemDeServico
    {
        private readonly List<ItemDeServico> _itens = new();
        public int Id { get; }
        public int ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }
        public DateOnly DataAbertura { get; }
        public StatusOS Status { get; private set; }
        public IReadOnlyCollection<ItemDeServico> Itens => _itens.AsReadOnly();

        private OrdemDeServico(int id, Cliente cliente)
        {
            Id = id;
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            ClienteId = cliente.Id;
            DataAbertura = DateOnly.FromDateTime(DateTime.UtcNow);
            Status = StatusOS.Aberta;
        }

        public static OrdemDeServico Create(int id, Cliente cliente)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente));
            var o = new OrdemDeServico(id, cliente);
            // synchronize bidirectional
            cliente.AddOrdemInternal(o);
            return o;
        }

        public void ChangeCliente(Cliente novoCliente)
        {
            if (novoCliente == null) throw new ArgumentNullException(nameof(novoCliente));
            if (novoCliente.Id == ClienteId) return;

            // remove from old
            Cliente.RemoveOrdemInternal(this);
            // set new
            Cliente = novoCliente;
            ClienteId = novoCliente.Id;
            // add to new
            novoCliente.AddOrdemInternal(this);
        }

        public void AddItem(ItemDeServico item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (Status == StatusOS.Concluida || Status == StatusOS.Cancelada) throw new InvalidOperationException("Cannot add items when ordem is concluded or canceled.");
            _itens.Add(item);
        }

        public void RemoveItem(ItemDeServico item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (Status == StatusOS.Concluida || Status == StatusOS.Cancelada) throw new InvalidOperationException("Cannot remove items when ordem is concluded or canceled.");
            _itens.Remove(item);
        }

        public decimal Total() => _itens.Sum(i => i.Subtotal());

        public void IniciarExecucao()
        {
            if (Status != StatusOS.Aberta) throw new InvalidOperationException("Só é possível iniciar execução quando a OS está Aberta.");
            if (!_itens.Any()) throw new InvalidOperationException("Cannot start execution without items.");
            Status = StatusOS.EmExecucao;
        }

        public void Concluir()
        {
            if (Status != StatusOS.EmExecucao) throw new InvalidOperationException("Só é possível concluir quando a OS está EmExecucao.");
            Status = StatusOS.Concluida;
        }

        public void Cancelar()
        {
            if (Status == StatusOS.Concluida) throw new InvalidOperationException("Cannot cancel a concluded order.");
            Status = StatusOS.Cancelada;
        }
    }
}
