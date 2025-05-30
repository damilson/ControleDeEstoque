namespace ControleDeEstoque.Server.Models
{
    public class ItensApi
    {
        public Guid Id { get; set; }
        public required string Descricao { get; set; }
        public required int Quantidade { get; set; }
        public required decimal Preco { get; set; }
    }
}
