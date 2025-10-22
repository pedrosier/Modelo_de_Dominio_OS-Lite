using System;
using Xunit;
using OSLite.Domain.Entities;
using OSLite.Domain.ValueObjects;

namespace OSLite.Domain.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void Money_nao_aceita_negativo()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Money(-1m));
        }
    }

    public class ItemDeServicoTests
    {
        [Fact]
        public void ItemDeServico_cria_valido_e_calcula_subtotal()
        {
            var m = new Money(10m);
            var item = new ItemDeServico("Troca de tela", 2, m, OSLite.Domain.CategoriaItem.Pecas);
            Assert.Equal(20m, item.Subtotal());
        }
    }

    public class OrdemTests
    {
        [Fact]
        public void OS_total_soma_subtotais_itens()
        {
            var cliente = new Cliente(1, "Joao");
            var ordem = cliente.OpenOrdem(100);
            ordem.AddItem(new ItemDeServico("Serv1", 1, new Money(5m)));
            ordem.AddItem(new ItemDeServico("Serv2", 3, new Money(2m)));
            Assert.Equal(11m, ordem.Total());
        }

        [Fact]
        public void OS_aberta_inicia_execucao_quando_tem_itens()
        {
            var cliente = new Cliente(2, "Maria");
            var ordem = cliente.OpenOrdem(101);
            ordem.AddItem(new ItemDeServico("Serv", 1, new Money(5m)));
            ordem.IniciarExecucao();
            Assert.Equal(OSLite.Domain.StatusOS.EmExecucao, ordem.Status);
        }

        [Fact]
        public void OS_aberta_nao_inicia_sem_itens()
        {
            var cliente = new Cliente(3, "Rui");
            var ordem = cliente.OpenOrdem(102);
            var ex = Assert.Throws<InvalidOperationException>(() => ordem.IniciarExecucao());
            Assert.Contains("without items", ex.Message);
        }

        [Fact]
        public void OS_nao_adiciona_itens_em_concluida_ou_cancelada()
        {
            var cliente = new Cliente(4, "Ana");
            var ordem = cliente.OpenOrdem(103);
            ordem.AddItem(new ItemDeServico("Serv",1,new Money(1m)));
            ordem.IniciarExecucao();
            ordem.Concluir();
            Assert.Throws<InvalidOperationException>(() => ordem.AddItem(new ItemDeServico("x",1,new Money(1m))));
            // cancel path
            var ordem2 = cliente.OpenOrdem(104);
            ordem2.AddItem(new ItemDeServico("s",1,new Money(1m)));
            ordem2.IniciarExecucao();
            ordem2.Cancelar();
            Assert.Throws<InvalidOperationException>(() => ordem2.RemoveItem(new ItemDeServico("s",1,new Money(1m))));
        }

        [Fact]
        public void Bidirecional_Cliente_adiciona_ordem_sincroniza_cliente_na_ordem_and_change_cliente_updates_collections()
        {
            var c1 = new Cliente(10, "C1");
            var c2 = new Cliente(11, "C2");
            var o = c1.OpenOrdem(200);
            Assert.Equal(c1.Id, o.ClienteId);
            Assert.Contains(o, c1.Ordens);
            o.ChangeCliente(c2);
            Assert.DoesNotContain(o, c1.Ordens);
            Assert.Contains(o, c2.Ordens);
            Assert.Equal(c2.Id, o.ClienteId);
        }
    }
}
