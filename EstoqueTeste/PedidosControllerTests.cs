using AutoMapper;
using ControleDeEstoque.Server.Controllers;
using ControleDeEstoque.Server.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Servicos.DTO;
using Servicos.Interfaces;

namespace EstoqueTeste
{
    [TestFixture]
    public class PedidosControllerTests
    {
        private IPedidosServico _servico;
        private IMapper _mapper;
        private PedidosController _controller;

        [SetUp]
        public void Setup()
        {
            _servico = Substitute.For<IPedidosServico>();
            _mapper = Substitute.For<IMapper>();
            _controller = new PedidosController(_servico, _mapper);
        }

        [Test]
        public async Task Get_DeveRetornarOkComLista()
        {
            var listaDTO = new List<PedidosDTO> { new PedidosDTO() };
            var listaAPI = new List<PedidosApi> { new PedidosApi() };

            _servico.BuscarAsync().Returns(listaDTO);
            _mapper.Map<List<PedidosApi>>(listaDTO).Returns(listaAPI);

            var result = await _controller.Get();

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetPorId_DeveRetornarOkComItem()
        {
            var id = Guid.NewGuid();
            var dto = new PedidosDTO { Id = id };
            var api = new PedidosApi { Id = id };

            _servico.BuscarPorIdAsync(id).Returns(dto);
            _mapper.Map<PedidosApi>(dto).Returns(api);

            var result = await _controller.Get(id);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Post_DeveRetornarOkComId()
        {
            var model = new PedidosApi();
            var dto = new PedidosDTO();
            var novoId = Guid.NewGuid();

            _mapper.Map<PedidosDTO>(model).Returns(dto);
            _servico.IncluirAsync(dto).Returns(novoId);

            var result = await _controller.Post(model) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(novoId, result.Value);
        }

        [Test]
        public async Task IncluirItemPedido_DeveRetornarMensagemDeSucesso()
        {
            var model = new PedidosItensApi();
            var dto = new PedidosItensDTO();

            _mapper.Map<PedidosItensDTO>(model).Returns(dto);

            var result = await _controller.IncluirItemPedido(model) as OkObjectResult;

            await _servico.Received().IncluirItemPedido(dto);
            Assert.NotNull(result);
            Assert.AreEqual("Item incluido com sucesso!", result.Value);
        }

        [Test]
        public async Task Put_DeveRetornarOk()
        {
            var model = new PedidosApi();
            var dto = new PedidosDTO();

            _mapper.Map<PedidosDTO>(model).Returns(dto);

            var result = await _controller.Put(model);

            await _servico.Received().AtualizarAsync(dto);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task FecharPedido_DeveRetornarMensagemDeSucesso()
        {
            var id = Guid.NewGuid();

            var result = await _controller.FecharPedido(id) as OkObjectResult;

            await _servico.Received().FecharPedido(id);
            Assert.NotNull(result);
            Assert.AreEqual("Pedido fechado com sucesso!", result.Value);
        }

        [Test]
        public async Task RemoverPedido_DeveRetornarOk()
        {
            var itemId = Guid.NewGuid();
            var pedidoId = Guid.NewGuid();

            var result = await _controller.RemoverPedido(itemId, pedidoId);

            await _servico.Received().RemoverItemPedido(itemId, pedidoId);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task Delete_DeveRetornarOk()
        {
            var id = Guid.NewGuid();

            var result = await _controller.Delete(id);

            await _servico.Received().RemoverAsync(id);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task Get_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _servico.BuscarAsync().Returns<Task<List<PedidosDTO>>>(_ => throw new Exception("Erro interno"));

            var result = await _controller.Get();

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task IncluirItemPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var model = new PedidosItensApi();
            _mapper.Map<PedidosItensDTO>(model).Returns(new PedidosItensDTO());
            _servico
                .When(x => x.IncluirItemPedido(Arg.Any<PedidosItensDTO>()))
                .Do(_ => throw new Exception("Erro ao incluir"));

            var result = await _controller.IncluirItemPedido(model);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
