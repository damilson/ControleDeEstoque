namespace Servicos.DTO
{
    public class PedidosItensDTO
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantidade { get; set; }

        public PedidosDTO Pedido { get; set; }
        public ItensDTO Item { get; set; }
    }
}
