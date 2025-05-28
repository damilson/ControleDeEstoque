using Entidades;
using Servicos.DTO;

namespace Servicos.Interfaces
{
    public interface ITokenServico : IServicoBase<UsuarioDTO, Usuario>
    {
        string GerarToken(Usuario usuario);
    }
}
