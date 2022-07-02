using EstacBem.Domain.Entities;
using EstacBem.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace EstacBem.Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Bolsao> Bolsoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Estadia> Estadias { get; set; }
        public DbSet<Precificacao> Precificacoes { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer("Server =.\\SQLEXPRESS; Database = EstacBem; Trusted_Connection = True; TrustServerCertificate = true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BolsaoConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new EstadiaConfiguration());
            modelBuilder.ApplyConfiguration(new PrecificacaoConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new VeiculoConfiguration());
        }
    }
}
