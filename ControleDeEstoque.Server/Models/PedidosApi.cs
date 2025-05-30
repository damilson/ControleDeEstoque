namespace ControleDeEstoque.Server.Models
{
    public class PedidosApi
    {
        public Guid Id { get; set; }

        public string Numero { get; set; }

        public string NomeCliente { get; set; }

        public List<ItensApi> Itens { get; set; }
    }
}
