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
            var usuarios = await BuscarAsync();
            var user = _mapper.Map<Usuario>(usuarios.FirstOrDefault(u => u.CPF == dto.CPF));

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, user.SenhaHash))
                throw new UnauthorizedAccessException("Credenciais inválidas");

            return _tokenService.GerarToken(user);
        }

        public override async Task<Guid> IncluirAsync(UsuarioDTO dto)
        {
            try
            {
                var usuarios = await BuscarAsync();
                var user = usuarios.FirstOrDefault(u => u.CPF == dto.CPF);

                if (user != null)
                    throw new Exception("O cpf informa já encontra-se cadastrado no sistema.");

                var entidade = await IncluirAsync(dto);

                return entidade;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override async Task<Guid> AtualizarAsync(UsuarioDTO dto)
        {
            {
                try
                {
                    var usuarios = await BuscarAsync();
                    var user = usuarios.FirstOrDefault(u => u.Id == dto.Id);

                    if (user == null)
                        throw new Exception("O usuário informado não esta cadastrado.");

                    user.Email = dto.Email;
                    user.DataNascimento = dto.DataNascimento;
                    user.CPF = dto.CPF;
                    user.Celular = dto.Celular;
                    user.Telefone = dto.Telefone;
                    user.Rg = dto.Rg;
                    user.Perfil = dto.Perfil;

                    var entidade = await AtualizarAsync(user);

                    return entidade;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public override Task ValidarOperacao(UsuarioDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
