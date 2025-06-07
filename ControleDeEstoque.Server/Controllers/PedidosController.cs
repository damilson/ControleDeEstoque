using AutoMapper;
using ControleDeEstoque.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTO;
using Servicos.Interfaces;

namespace ControleDeEstoque.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidosServico _pedidosServico;
        private readonly IMapper _mapper;

        public PedidosController(IPedidosServico pedidosServico, IMapper mapper)
        {
            _pedidosServico = pedidosServico;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_mapper.Map<List<PedidosApi>>(await _pedidosServico.BuscarAsync()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var pedidos = _mapper.Map<PedidosApi>(await _pedidosServico.BuscarPorIdAsync(id));
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PedidosApi model)
        {
            try
            {
                var id = await _pedidosServico.IncluirAsync(_mapper.Map<PedidosDTO>(model));
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("item")]
        public async Task<IActionResult> IncluirItemPedido([FromBody] PedidosItensApi model)
        {
            try
            {
                await _pedidosServico.IncluirItemPedido(_mapper.Map<PedidosItensDTO>(model));
                return Ok("Item incluido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] PedidosApi model)
        {
            try
            {
                await _pedidosServico.AtualizarAsync(_mapper.Map<PedidosDTO>(model));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/fechar-pedido")]
        public async Task<IActionResult> FecharPedido(Guid id)
        {
            try
            {
                await _pedidosServico.FecharPedido(id);

                return Ok("Pedido fechado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{itemId}/item/{pedidoId}/pedido")]
        public async Task<IActionResult> RemoverPedido(Guid itemId, Guid pedidoId)
        {
            try
            {
                await _pedidosServico.RemoverItemPedido(itemId,pedidoId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _pedidosServico.RemoverAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
