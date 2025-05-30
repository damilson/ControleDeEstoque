namespace Entidades
{
    public class Pedidos : IEntity
    {
        public Guid Id { get; set; }

        public required string Numero { get; set; }

        public required string NomeCliente { get; set; }

        public virtual required List<Itens> Itens { get; set; }
    }
}
