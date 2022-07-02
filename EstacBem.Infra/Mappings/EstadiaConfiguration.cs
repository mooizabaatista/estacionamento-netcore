using EstacBem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstacBem.Infra.Mappings
{
    public class EstadiaConfiguration : IEntityTypeConfiguration<Estadia>
    {
        public void Configure(EntityTypeBuilder<Estadia> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Data)
                .IsRequired();

            builder.Property(x => x.Entrada)
                .IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.ValorPrimeira)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.ValorPrimeira)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.ValorTotal)
                .HasPrecision(18, 2);
        }
    }
}
