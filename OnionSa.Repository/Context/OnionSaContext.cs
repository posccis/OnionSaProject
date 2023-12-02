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


    }
}
