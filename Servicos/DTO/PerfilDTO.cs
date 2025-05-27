using Util;

namespace Servicos.DTO
{
    public class PerfilDTO
    {
        public Guid Id { get; set; }

        public TipoPerfil Tipo { get; set; }

        public virtual ICollection<UsuarioDTO>? Usuarios { get; set; }
    }
}
