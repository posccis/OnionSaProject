using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using OnionSa.Domain.Models;
using Microsoft.Extensions.Configuration;



namespace OnionSa.Repository.Context
{
    public class OnionSaContext : DbContext
    {
        public OnionSaContext(DbContextOptions<OnionSaContext> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Produto>().HasData(
                new Produto {ProdutoId = 1, Titulo = "Celular", Preco= 1000},
                new Produto { ProdutoId = 2, Titulo = "Notebook", Preco= 3000 },
                new Produto { ProdutoId = 3, Titulo = "Televisão", Preco = 5000 }
            );
        }

    }
}
