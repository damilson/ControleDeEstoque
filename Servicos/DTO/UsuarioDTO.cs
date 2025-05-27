using Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Servicos.DTO
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }

        public string NomeCompleto { get; set; }

        public string CPF { get; set; }

        public string RG { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public string Sexo { get; set; }

        public string Senha { get; set; }
        public string SenhaHash { get; set; }

        public int PerfilId { get; set; }

        [JsonIgnore]
        public virtual PerfilDTO Perfil { get; set; }

        public virtual EnderecoDTO Endereco { get; set; }
    }
}
