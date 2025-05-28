using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Util;

namespace Entidades
{
    public class Usuario : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string NomeCompleto { get; set; }

        [Required]
        [MaxLength(12)]
        public string CPF { get; set; }

        [MaxLength(20)]
        public string RG { get; set; }

        public DateTime? DataNascimento { get; set; }

        [MaxLength(15)]
        public string Telefone { get; set; }

        [MaxLength(15)]
        public string Celular { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(1)] // M/F/O (masculino/feminino/outro)
        public string Sexo { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Senha { get; set; }
        public string SenhaHash { get; set; }

        public TipoPerfil Perfil { get; set; }

        public virtual Endereco Endereco { get; set; }
    }
}
