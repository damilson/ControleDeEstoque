using AutoMapper;
using ControleDeEstoque.Server.Controllers;
using ControleDeEstoque.Server.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Servicos.DTO;
using Servicos.Interfaces;

namespace EstoqueTeste
{
    [TestFixture]
    public class ItensControllerTests
    {
        private IItensServico _servico;
        private IMapper _mapper;
        private ItensController _controller;

        [SetUp]
        public void Setup()
        {
            _servico = Substitute.For<IItensServico>();
            _mapper = Substitute.For<IMapper>();
            _controller = new ItensController(_servico, _mapper);
        }

        [Test]
        public async Task Get_DeveRetornarOkComLista()
        {
            var listaDTO = new List<ItensDTO> { new ItensDTO() };
            var listaAPI = new List<ItensApi> { new ItensApi() };

            _servico.BuscarAsync().Returns(listaDTO);
            _mapper.Map<List<ItensApi>>(listaDTO).Returns(listaAPI);

            var result = await _controller.Get();

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetPorId_DeveRetornarOkComItem()
        {
            var id = Guid.NewGuid();
            var dto = new ItensDTO { Id = id };
            var api = new ItensApi { Id = id };

            _servico.BuscarPorIdAsync(id).Returns(dto);
            _mapper.Map<ItensApi>(dto).Returns(api);

            var result = await _controller.Get(id);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Post_DeveRetornarOkComId()
        {
            var model = new ItensApi();
            var dto = new ItensDTO();
            var idCriado = Guid.NewGuid();

            _mapper.Map<ItensDTO>(model).Returns(dto);
            _servico.IncluirAsync(dto).Returns(idCriado);

            var result = await _controller.Post(model) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(idCriado, result.Value);
        }

        [Test]
        public async Task Put_DeveRetornarOk()
        {
            var model = new ItensApi();
            var dto = new ItensDTO();

            _mapper.Map<ItensDTO>(model).Returns(dto);

            var result = await _controller.Put(model);

            await _servico.Received().AtualizarAsync(dto);
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
            _servico.BuscarAsync().Returns<Task<List<ItensDTO>>>(_ => throw new Exception("Erro interno"));

            var result = await _controller.Get();

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}