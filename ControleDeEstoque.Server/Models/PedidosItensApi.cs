using System.Text.Json.Serialization;

namespace ControleDeEstoque.Server.Models
{
    public class PedidosItensApi
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantidade { get; set; }

        [JsonIgnore]
        public PedidosApi? Pedido { get; set; }
        [JsonIgnore]
        public ItensApi? Item { get; set; }
    }
}
