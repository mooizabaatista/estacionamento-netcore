using EstacBem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstacBem.Infra.Mappings
{
    public class PrecificacaoConfiguration : IEntityTypeConfiguration<Precificacao>
    {
        public void Configure(EntityTypeBuilder<Precificacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DataHora)
                .IsRequired();

            builder.Property(x => x.Observacao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ValorPrimeira)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.ValorDemais)
                .IsRequired()
                .HasPrecision(18, 2);
        }
    }
}
