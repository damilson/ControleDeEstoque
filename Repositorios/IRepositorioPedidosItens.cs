namespace Repositorios
{
    public interface IRepositorioPedidosItens
    {
        Task RemoveItensPedido(Guid idPedido);
    }
}
