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
            
            CreateMap<ItensApi, ItensDTO>().ReverseMap();
            
            CreateMap<PedidosApi, PedidosDTO>().ReverseMap()
                .ForMember(dest => dest.Itens, ori => ori.MapFrom(src => src.Itens))
                .ReverseMap();

            CreateMap<PedidosItensApi, PedidosItensDTO>()
                .ForMember(dest => dest.PedidoId, ori => ori.MapFrom(src => src.PedidoId))
                .ForMember(dest => dest.ItemId, ori => ori.MapFrom(src => src.ItemId))
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ReverseMap();

        }

        private List<ItensApi> ConverteLista(List<PedidosItensDTO> itens)
        {
            var listaItensApi = new List<ItensApi>();

            foreach (var item in itens)
            {
                listaItensApi.Add(new ItensApi
                {
                    Id = item.Item.Id,
                    Descricao = item.Item.Descricao,
                    Preco = item.Item.Preco,
                    Quantidade = item.Item.Quantidade
                });
            }

            return listaItensApi;
        }
    }
}
