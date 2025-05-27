using Entidades;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Servicos.DTO
{
    public class EnderecoDTO
    {
        public Guid Id { get; set; }

        public string Rua { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string CEP { get; set; }

        [JsonIgnore]
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public virtual UsuarioDTO Usuario { get; set; }
    }
}
