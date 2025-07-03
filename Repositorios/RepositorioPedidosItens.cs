
using Entidades;
using Repositorios.Generico;

namespace Repositorios
{
    public class RepositorioPedidosItens : IRepositorioPedidosItens
    {
        private readonly IRepositorioGenerico<PedidosItens> repositorioGenerico;

        public RepositorioPedidosItens(Contexto.Contexto contexto)
        {
            repositorioGenerico = new RepositorioGenerico<PedidosItens>(contexto);    
        }
        
        public Task RemoveItensPedido(Guid idPedido)
        {
            throw new NotImplementedException();
        }
    }
}
