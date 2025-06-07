using Entidades;
using Servicos.DTO;

namespace Servicos.Interfaces
{
    public interface IPedidosServico : IServicoBase<PedidosDTO,Pedidos>
    {
        Task<Guid> FecharPedido(Guid pedidoId);
        Task IncluirItemPedido(PedidosItensDTO pedidoItens);
        Task ReabrirPedido(Guid pedidoItens);
        Task RemoverItemPedido(Guid itemId,Guid pedidoId);
    }
}
