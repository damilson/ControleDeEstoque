using Entidades;
using Servicos.DTO;

namespace Servicos.Interfaces
{
    public interface ITokenServico : IServicoBase<UsuarioDTO>
    {
        string GerarToken(Usuario usuario);
    }
}
