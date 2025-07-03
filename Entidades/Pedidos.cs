using Util;

namespace Entidades
{
    public class Pedidos : IEntity
    {
        public Guid Id { get; set; }

        public required string Numero { get; set; }

        public required string NomeCliente { get; set; }

        public required SituacaoPedido SituacaoPedido { get; set; }

        public virtual required List<PedidosItens> Itens { get; set; }
    }
}
