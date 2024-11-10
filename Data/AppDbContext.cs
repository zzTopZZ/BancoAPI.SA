using BancoAPI.SA.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.SA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base( options )
        {
        }

        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<ContaCorrenteModel> ContaCorrentes { get;set; }
        public DbSet<ExtratoModel> Extratos { get; set; }
        public DbSet<TipoTransacaoModel> TipoTransacao { get; set; }
        public DbSet<TransacaoModel> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoTransacaoModel>().HasData(
                    new TipoTransacaoModel { Id = 1, Descricao = "Depósito" },
                    new TipoTransacaoModel { Id = 2, Descricao = "Saque" },
                    new TipoTransacaoModel { Id = 3, Descricao = "Transferência" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
