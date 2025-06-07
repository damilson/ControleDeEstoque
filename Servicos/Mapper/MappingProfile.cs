using AutoMapper;
using Entidades;
using Servicos.DTO;

namespace Servicos.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Itens, ItensDTO>().ReverseMap();
            CreateMap<Pedidos, PedidosDTO>().ReverseMap();
            CreateMap<PedidosItens, PedidosItensDTO>().ReverseMap();
        }
    }
}
