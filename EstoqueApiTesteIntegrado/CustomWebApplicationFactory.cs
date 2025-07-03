using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.IO;

namespace EstoqueApiTesteIntegrado
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseContentRoot(GetContentRootPath());
            return base.CreateHost(builder);
        }

        private string GetContentRootPath()
        {
            // Aponta para a pasta onde está o .csproj do projeto principal
            var projectDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../ControleDeEstoque.Server"));
            return projectDir;
        }
    }
}
