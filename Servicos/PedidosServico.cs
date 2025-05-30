using AutoMapper;
using Entidades;
using Repositorios.Contexto;
using Servicos.DTO;
using Servicos.Interfaces;

namespace Servicos
{
    public class PedidosServico : ServicoBase<PedidosDTO, Pedidos>, IPedidosServico
    {
        public PedidosServico(Contexto contexto, IMapper mapper) : base(contexto, mapper)
        {
        }
    }
}
