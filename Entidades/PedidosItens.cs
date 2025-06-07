using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class PedidosItens : IEntity
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantidade { get; set; }


        [ForeignKey("PedidoId")]
        public virtual Pedidos Pedido { get; set; }

        [ForeignKey("ItemId")]
        public virtual Itens Item { get; set; }
    }
}
