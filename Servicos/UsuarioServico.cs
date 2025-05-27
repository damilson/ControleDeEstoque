using AutoMapper;
using Entidades;
using Repositorios.Contexto;
using Repositorios.Generico;
using Servicos.DTO;
using Servicos.Interfaces;

namespace Servicos
{
    public class UsuarioServico : ServicoBase<UsuarioDTO, Usuario>, IUsuarioServico
    {
        private readonly TokenService _tokenService;

        public UsuarioServico(Contexto contexto, IMapper mapper) : base(contexto, mapper)
        {
        }

        public async Task<string> AutenticarAsync(UsuarioDTO dto)
        {
            var usuarios = await GetAllAsync();
            var user = usuarios.FirstOrDefault(u => u.CPF == dto.CPF);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, user.SenhaHash))
                throw new UnauthorizedAccessException("Credenciais inválidas");

            return _tokenService.GerarToken(user);
        }
    }
}
