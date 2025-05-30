namespace Entidades
{
    public class Itens : IEntity
    {
        public Guid Id { get; set; }
        public required string Descricao { get; set; }
        public required int Quantidade { get; set; }
        public required decimal Preco { get; set; }

    }
}
