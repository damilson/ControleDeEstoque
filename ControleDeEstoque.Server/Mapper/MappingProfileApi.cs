using AutoMapper;
using ControleDeEstoque.Server.Models;
using Servicos.DTO;

namespace ControleDeEstoque.Server.Mapper
{
    public class MappingProfileApi : Profile
    {
        public MappingProfileApi()
        {
            CreateMap<UsuarioApi, UsuarioDTO>().ReverseMap();
            CreateMap<PedidosApi, ItensDTO>().ReverseMap();
            CreateMap<PedidosApi, PedidosDTO>().ReverseMap();
        }
    }
}
