using ControleDeEstoque.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTO;
using Servicos.Interfaces;

namespace ControleDeEstoque.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _servico;

        public UsuarioController(IUsuarioServico servico)
        {
            _servico = servico;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginApi model)
        {
            var dto = new UsuarioDTO()
            {
                CPF = model.Cpf,
                Senha = model.Senha,
            };
            var token = await _servico.AutenticarAsync(dto);
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("login/dados")]
        public IActionResult UsuarioLogado()
        {
            var email = User?.Identity?.Name;
            return Ok(new { logado = true, email });
        }
    }
}
