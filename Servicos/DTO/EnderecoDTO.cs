namespace Servicos.DTO
{
    public class EnderecoDTO
    {
        public Guid Id { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string CEP { get; set; }

        public Guid UsuarioId { get; set; }
        
        public UsuarioDTO Usuario { get; set; }
    }
}
