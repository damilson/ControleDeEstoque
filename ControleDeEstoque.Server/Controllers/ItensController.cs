using AutoMapper;
using ControleDeEstoque.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTO;
using Servicos.Interfaces;

namespace ControleDeEstoque.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensController : ControllerBase
    {
        private readonly IItensServico _itensServico;
        private readonly IMapper _mapper;

        public ItensController(IItensServico itensServico, IMapper mapper)
        {
            _itensServico = itensServico;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_mapper.Map<List<ItensApi>>(await _itensServico.BuscarAsync()));
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
                return Ok(_mapper.Map<ItensApi>(await _itensServico.BuscarPorIdAsync(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItensApi model)
        {
            try
            {
                var id = await _itensServico.IncluirAsync(_mapper.Map<ItensDTO>(model));
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] ItensApi model)
        {
            try
            {
                await _itensServico.AtualizarAsync(_mapper.Map<ItensDTO>(model));
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
                await _itensServico.RemoverAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
