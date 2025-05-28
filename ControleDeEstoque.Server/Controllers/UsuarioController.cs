using AutoMapper;
using ControleDeEstoque.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTO;
using Servicos.Interfaces;

namespace ControleDeEstoque.Server.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _servicoUsuario;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioServico servico, IMapper mapper)
        {
            _servicoUsuario = servico;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginApi model)
        {
            var dto = new UsuarioDTO()
            {
                CPF = model.Cpf,
                Senha = model.Senha,
            };
            var token = await _servicoUsuario.AutenticarAsync(dto);
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("login/dados")]
        public IActionResult UsuarioLogado()
        {
            var email = User?.Identity?.Name;
            return Ok(new { logado = true, email });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(UsuarioApi model)
        {
            var usuario = await _servicoUsuario.IncluirAsync(_mapper.Map<UsuarioDTO>(model));
            return Ok(usuario);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UsuarioApi model)
        {
            var usuario = await _servicoUsuario.AutenticarAsync(_mapper.Map<UsuarioDTO>(model));
            return Ok(usuario);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _servicoUsuario.RemoverAsync(id);
                return Ok("Usuário removido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
