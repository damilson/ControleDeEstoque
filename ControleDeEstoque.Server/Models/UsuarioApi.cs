namespace ControleDeEstoque.Server.Models
{
    public class UsuarioApi
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

        public EnderecoApi Endereco { get; set; }
    }
}
