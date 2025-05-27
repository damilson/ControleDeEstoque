using Servicos.DTO;

namespace Servicos.Interfaces
{
    public interface IUsuarioServico
    {
        Task<string> AutenticarAsync(UsuarioDTO dto);
    }
}
