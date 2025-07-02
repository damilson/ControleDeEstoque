using Entidades;

namespace Servicos.Interfaces
{
    public interface ITokenServico 
    {
        string GerarToken(Usuario usuario);
    }
}
