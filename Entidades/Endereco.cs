using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Endereco : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(150)]
        public required string Logradouro { get; set; }

        [MaxLength(50)]
        public required string Numero { get; set; }

        [MaxLength(100)]
        public string? Complemento { get; set; }

        [MaxLength(100)]
        public required string Bairro { get; set; }

        [MaxLength(100)]
        public required string Cidade { get; set; }

        [MaxLength(2)]
        public required string Uf { get; set; }

        [MaxLength(10)]
        public required string CEP { get; set; }

        public Guid UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
