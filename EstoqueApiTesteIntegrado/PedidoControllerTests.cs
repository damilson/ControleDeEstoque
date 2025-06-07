using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;

namespace EstoqueApiTesteIntegrado.Tests
{
    public class PedidoControllerTests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            var factory = new CustomWebApplicationFactory();
            _client = factory.CreateClient();
        }

        [Test]
        public async Task GetTodosPedidos_DeveRetornar200()
        {
            var response = await _client.GetAsync("/api/pedido");

            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task PostPedido_DeveRetornar201()
        {
            var novoPedido = new
            {
                Cliente = "Teste Cliente",
                Valor = 150.00M
            };

            var response = await _client.PostAsJsonAsync("/api/pedido", novoPedido);
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task GetPedidoPorId_DeveRetornar200()
        {
            var novoPedido = new { Cliente = "Teste Consulta", Valor = 100.00M };
            var postResponse = await _client.PostAsJsonAsync("/api/pedido", novoPedido);
            var created = await postResponse.Content.ReadFromJsonAsync<PedidoResposta>();

            var getResponse = await _client.GetAsync($"/api/pedido/{created.Id}");
            NUnit.Framework.Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task PutPedido_DeveRetornar204()
        {
            var novoPedido = new { Cliente = "Teste Update", Valor = 120.00M };
            var postResponse = await _client.PostAsJsonAsync("/api/pedido", novoPedido);
            var created = await postResponse.Content.ReadFromJsonAsync<PedidoResposta>();

            var atualizacao = new { Id = created.Id, Cliente = "Cliente Atualizado", Valor = 130.00M };
            var putResponse = await _client.PutAsJsonAsync($"/api/pedido/{created.Id}", atualizacao);

            NUnit.Framework.Assert.That(putResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task DeletePedido_DeveRetornar204()
        {
            var novoPedido = new { Cliente = "Teste Delete", Valor = 99.00M };
            var postResponse = await _client.PostAsJsonAsync("/api/pedido", novoPedido);
            var created = await postResponse.Content.ReadFromJsonAsync<PedidoResposta>();

            var deleteResponse = await _client.DeleteAsync($"/api/pedido/{created.Id}");
            NUnit.Framework.Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        private class PedidoResposta
        {
            public int Id { get; set; }
            public string Cliente { get; set; }
            public decimal Valor { get; set; }
        }
    }
}
