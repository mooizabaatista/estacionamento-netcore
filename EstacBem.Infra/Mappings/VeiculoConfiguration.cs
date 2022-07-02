using EstacBem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstacBem.Infra.Mappings
{
    public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Placa)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Marca)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Modelo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Cor)
                .IsRequired()
                .HasMaxLength(20);

            //Relacionamentos
            builder.HasMany(x => x.Estadias).WithOne(x => x.Veiculo).HasForeignKey(x => x.VeiculoId);
        }
    }
}
