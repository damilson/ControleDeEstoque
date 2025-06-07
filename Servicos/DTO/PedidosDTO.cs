using Util;

namespace Servicos.DTO
{
    public class PedidosDTO
    {
        public Guid Id { get; set; }

        public string Numero { get; set; }

        public string NomeCliente { get; set; }

        public SituacaoPedido SituacaoPedido { get; set; }

        public List<PedidosItensDTO> Itens { get; set; }
    }
}
