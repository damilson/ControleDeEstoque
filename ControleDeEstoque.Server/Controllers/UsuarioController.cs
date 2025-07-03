using AutoMapper;
using ControleDeEstoque.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTO;
using Servicos.Interfaces;
using System.Security.Claims;

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

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginApi model)
        //{
        //    try
        //    {

        //        var dto = new UsuarioDTO()
        //        {
        //            CPF = model.Cpf,
        //            Senha = model.Senha,
        //        };
        //        var token = await _servicoUsuario.AutenticarAsync(dto);
        //        return Ok(new { token });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[Authorize]
        //[HttpGet("login/dados")]
        //public IActionResult UsuarioLogado()
        //{
        //    var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    if (!Guid.TryParse(idClaim, out Guid usuarioId))
        //    {
        //        // ID inválido ou ausente
        //        return Unauthorized(new { mensagem = "Usuário inválido." });
        //    }

        //    var idUsuario = Guid.Parse(idClaim);
        //    var usuario = _servicoUsuario.BuscarPorIdAsync(idUsuario);
        //    return Ok(new { logado = true, usuario });
        //}

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
