using System.ComponentModel.DataAnnotations;
using Util;

namespace Entidades
{
    public class Perfil : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public required TipoPerfil Tipo { get; set; }

        public virtual ICollection<Usuario>? Usuarios { get; set; }
    }
}
