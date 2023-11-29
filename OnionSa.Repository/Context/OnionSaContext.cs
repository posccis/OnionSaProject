using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using OnionSa.Domain.Models;


namespace OnionSa.Repository.Context
{
    public class OnionSaContext : DbContext
    {
        private string _strCnx;
        public OnionSaContext()
        {
            _strCnx = "Server=tcp:onion-brazil.database.windows.net,1433;Initial Catalog=OnionSA;Persist Security Info=False;User ID=Posccis;Password=hubcount#2023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_strCnx);
            optionsBuilder.UseExceptionProcessor();
        }
    }
}
