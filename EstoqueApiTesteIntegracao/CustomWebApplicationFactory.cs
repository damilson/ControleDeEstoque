using Entidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositorios.Contexto;
using System.Linq;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test"); // ESSENCIAL

        // Se quiser popular a base:
        builder.ConfigureServices(services =>
        {
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Contexto>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            // Seed de teste
            var pedido = new Pedidos
            {
                Id = Guid.NewGuid(),
                NomeCliente = "Cliente Teste",
                SituacaoPedido = Util.SituacaoPedido.ABERTO,
                Numero = "123456",
                Itens = new List<PedidosItens>()
            };

            db.PEDIDOS.Add(pedido);
            db.PEDIDOSITENS.Add(new PedidosItens
            {
                Id = Guid.NewGuid(),
                PedidoId = pedido.Id,
                ItemId = Guid.NewGuid(),
                Quantidade = 10
            });

            db.SaveChanges();
        });
    }
}