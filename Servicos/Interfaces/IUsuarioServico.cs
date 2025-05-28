using Entidades;
using Servicos.DTO;

namespace Servicos.Interfaces
{
    public interface IUsuarioServico : IServicoBase<UsuarioDTO, Usuario>
    {
        Task<string> AutenticarAsync(UsuarioDTO dto);
    }
}
