using AutoMapper;
using Entidades;
using Repositorios.Contexto;
using Servicos.DTO;
using Servicos.Interfaces;

namespace Servicos
{
    public class ItensServico : ServicoBase<ItensDTO, Itens>, IItensServico
    {
        public ItensServico(Contexto contexto, IMapper mapper) : base(contexto, mapper)
        {
        }
    }
}
