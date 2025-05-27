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
        }
    }
}
