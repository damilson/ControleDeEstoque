using ControleDeEstoque.Server.Models;
using ControleDeEstoque.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTO;
using Servicos.Interfaces;
using TokenService = ControleDeEstoque.Server.Services.TokenService;

namespace ControleDeEstoque.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioServico _servicoUsuario;
        private readonly IConfiguration _config;
        private readonly InMemoryRefreshTokenStore _store;

        public AuthController(IConfiguration config, InMemoryRefreshTokenStore store, IUsuarioServico servicoUsuario)
        {
            _config = config;
            _store = store;
            _servicoUsuario = servicoUsuario;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var dto = new UsuarioDTO()
            {
                CPF = request.Username,
                Senha = request.Password,
            };

            var accessToken =  await _servicoUsuario.AutenticarAsync(dto);
            var refreshToken = TokenService.GenerateRefreshToken();

            _store.Save(request.Username, refreshToken);

            return Ok(new TokenResponse { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost("refresh-token")]
        public IActionResult Refresh([FromBody] RefreshTokenRequest req)
        {
            var user = _store.GetUsername(req.RefreshToken);
            if (user == null)
                return Unauthorized();

            var newAccessToken = TokenService.GenerateAccessToken(user, _config["Jwt:Key"], 15);
            var newRefreshToken = TokenService.GenerateRefreshToken();

            _store.Replace(user, req.RefreshToken, newRefreshToken);

            return Ok(new TokenResponse { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] RefreshTokenRequest req)
        {
            _store.Remove(req.RefreshToken);
            return Ok();
        }

        [HttpGet("secure-data")]
        [Authorize]
        public IActionResult GetData()
        {
            return Ok("Você acessou um endpoint protegido!");
        }
    }
}