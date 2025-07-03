using Util;

namespace ControleDeEstoque.Server.Models
{
    public class PedidosApi
    {
        public Guid Id { get; set; }

        public string Numero { get; set; }

        public string NomeCliente { get; set; }

        public SituacaoPedido SituacaoPedido { get; set; }

        public List<PedidosItensApi> Itens { get; set; }
    }
}
