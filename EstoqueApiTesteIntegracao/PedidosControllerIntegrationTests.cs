using System.Net;
using System.Net.Http.Json;

namespace EstoqueApiTesteIntegracao
{
    public class PedidosControllerIntegrationTests
    {
        private WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<MeuDbContexto>));
                        if (descriptor != null)
                            services.Remove(descriptor);

                        services.AddDbContext<MeuDbContexto>(options =>
                        {
                            options.UseInMemoryDatabase("BancoDeTestes");
                        });

                        var sp = services.BuildServiceProvider();
                        using (var scope = sp.CreateScope())
                        {
                            var db = scope.ServiceProvider.GetRequiredService<MeuDbContexto>();
                            db.Database.EnsureCreated();

                            var pedido = new PedidosDTO
                            {
                                Id = Guid.NewGuid(),
                                Cliente = "Cliente Teste",
                                Data = DateTime.UtcNow
                            };

                            db.Pedidos.Add(pedido);
                            db.SaveChanges();
                        }
                    });
                });

            _client = _factory.CreateClient();
        }

        [Test]
        public async Task Get_DeveRetornarPedidos()
        {
            var response = await _client.GetAsync("/api/pedidos");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var pedidos = await response.Content.ReadFromJsonAsync<List<PedidosApi>>();
            Assert.NotNull(pedidos);
            Assert.IsNotEmpty(pedidos);
        }

        [Test]
        public async Task Post_DeveCriarPedido()
        {
            var novoPedido = new PedidosApi
            {
                Cliente = "Novo Cliente",
                Data = DateTime.UtcNow
            };

            var response = await _client.PostAsJsonAsync("/api/pedidos", novoPedido);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var id = await response.Content.ReadFromJsonAsync<Guid>();
            Assert.AreNotEqual(Guid.Empty, id);
        }

        [Test]
        public async Task Put_DeveAtualizarPedido()
        {
            var response = await _client.GetAsync("/api/pedidos");
            var pedidos = await response.Content.ReadFromJsonAsync<List<PedidosApi>>();
            var pedido = pedidos.First();

            pedido.Cliente = "Cliente Atualizado";

            var putResponse = await _client.PutAsJsonAsync("/api/pedidos", pedido);
            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode);
        }

        [Test]
        public async Task FecharPedido_DeveRetornarOk()
        {
            var pedidos = await _client.GetFromJsonAsync<List<PedidosApi>>("/api/pedidos");
            var pedidoId = pedidos.First().Id;

            var response = await _client.PutAsync($"/api/pedidos/{pedidoId}/fechar-pedido", null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task Delete_DeveRemoverPedido()
        {
            var novoPedido = new PedidosApi
            {
                Cliente = "Excluir Cliente",
                Data = DateTime.UtcNow
            };

            var postResponse = await _client.PostAsJsonAsync("/api/pedidos", novoPedido);
            var id = await postResponse.Content.ReadFromJsonAsync<Guid>();

            var deleteResponse = await _client.DeleteAsync($"/api/pedidos/{id}");
            Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [Test]
        public async Task PostItem_DeveIncluirItemNoPedido()
        {
            var pedidos = await _client.GetFromJsonAsync<List<PedidosApi>>("/api/pedidos");
            var pedidoId = pedidos.First().Id;

            var item = new PedidosItensApi
            {
                PedidoId = pedidoId,
                Produto = "Produto Teste",
                Quantidade = 2,
                ValorUnitario = 100
            };

            var response = await _client.PostAsJsonAsync("/api/pedidos/item", item);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task DeleteItem_DeveRemoverItemDoPedido()
        {
            // Cria pedido
            var novoPedido = new PedidosApi
            {
                Cliente = "Com Item",
                Data = DateTime.UtcNow
            };
            var postResponse = await _client.PostAsJsonAsync("/api/pedidos", novoPedido);
            var pedidoId = await postResponse.Content.ReadFromJsonAsync<Guid>();

            // Adiciona item
            var item = new PedidosItensApi
            {
                PedidoId = pedidoId,
                Produto = "Produto para Remover",
                Quantidade = 1,
                ValorUnitario = 10
            };
            var itemResponse = await _client.PostAsJsonAsync("/api/pedidos/item", item);
            Assert.AreEqual(HttpStatusCode.OK, itemResponse.StatusCode);

            // Busca ID real do item no banco (simulado)
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MeuDbContexto>();
            var itemId = db.PedidosItens.First(i => i.Produto == "Produto para Remover").Id;

            // Remove o item
            var deleteResponse = await _client.DeleteAsync($"/api/pedidos/{itemId}/item/{pedidoId}/pedido");
            Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);
        }
    }
}
