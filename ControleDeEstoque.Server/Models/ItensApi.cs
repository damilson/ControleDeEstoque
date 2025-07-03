namespace ControleDeEstoque.Server.Models
{
    public class ItensApi
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
