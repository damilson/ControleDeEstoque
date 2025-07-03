using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection.Emit;

namespace Repositorios.Contexto
{
    public class Contexto : DbContext
    {
        public static readonly LoggerFactory loggerFactory = new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });

        public Contexto(DbContextOptions<Contexto> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.UseSqlServer("Server=10.33.213.15\\homologacao;DataBase=S4EVendaOnlineHomologacao;User ID=s4euser; Password=s4e8ashg65hd8656455;");
        }

        public DbSet<Usuario> USUARIOS { get; set; }
        public DbSet<Endereco> ENDERECOS { get; set; }
        public DbSet<Perfil> PERFILS { get; set; }
        public DbSet<Pedidos> PEDIDOS { get; set; }
        public DbSet<Itens> ITENS { get; set; }
        public DbSet<PedidosItens> PEDIDOSITENS { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
