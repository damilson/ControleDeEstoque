namespace ControleDeEstoque.Server.Models
{
    public class PedidosApi
    {
        public Guid Id { get; set; }
        public required string Descricao { get; set; }
        public required int Quantidade { get; set; }
        public required decimal Preco { get; set; }
    }
}
