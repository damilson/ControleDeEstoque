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

        public override async Task<Guid> IncluirAsync(UsuarioDTO dto)
        {
            try
            {
                var usuarios = await GetAllAsync();
                var user = usuarios.FirstOrDefault(u => u.CPF == dto.CPF);

                if (user != null)
                    throw new Exception("O cpf informa já encontra-se cadastrado no sistema.");

                var entidade = await CreatedAsync(_mapper.Map<Usuario>(dto));

                return entidade.Id;
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
                    var usuarios = await GetAllAsync();
                    var user = usuarios.FirstOrDefault(u => u.Id == dto.Id);

                    if (user == null)
                        throw new Exception("O usuário informado não esta cadastrado.");

                    user.Email = dto.Email;
                    user.DataNascimento = dto.DataNascimento;
                    user.CPF = dto.CPF;
                    user.Celular = dto.Celular;
                    user.Telefone = dto.Telefone;
                    user.RG = dto.RG;
                    user.Perfil = dto.Perfil;

                    var entidade = await UpdateAsync(user);

                    return entidade.Id;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
