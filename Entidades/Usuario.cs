using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Util;

namespace Entidades
{
    public class Usuario : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(250)]
        public required string Nome { get; set; }

        [MaxLength(12)]
        public required string CPF { get; set; }

        [MaxLength(20)]
        public string Rg { get; set; }

        public required DateTime DataNascimento { get; set; }

        [MaxLength(15)]
        public string Telefone { get; set; }

        [MaxLength(15)]
        public string Celular { get; set; }

        [MaxLength(100)]
        public required string Email { get; set; }

        [MaxLength(1)] // M/F/O (masculino/feminino/outro)
        public required string Sexo { get; set; }
        
        [MaxLength(100)]
        public required string Senha { get; set; }
        public required string SenhaHash { get; set; }

        public TipoPerfil Perfil { get; set; }

        public virtual Endereco Endereco { get; set; }
    }
}
