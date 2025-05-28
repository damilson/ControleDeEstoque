using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Endereco : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Logradouro { get; set; }

        [Required]
        [MaxLength(50)]
        public string Numero { get; set; }

        [MaxLength(100)]
        public string Complemento { get; set; }

        [Required]
        [MaxLength(100)]
        public string Bairro { get; set; }

        [Required]
        [MaxLength(100)]
        public string Cidade { get; set; }

        [Required]
        [MaxLength(2)]
        public string Estado { get; set; }

        [Required]
        [MaxLength(10)]
        public string CEP { get; set; }

        public Guid UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
