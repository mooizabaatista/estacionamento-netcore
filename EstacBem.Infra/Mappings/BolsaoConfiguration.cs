using EstacBem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstacBem.Infra.Mappings
{
    public class BolsaoConfiguration : IEntityTypeConfiguration<Bolsao>
    {
        public void Configure(EntityTypeBuilder<Bolsao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.QtdVagas)
                .IsRequired();

            //Relacionamentos
            builder.HasMany(x => x.Estadias).WithOne(x => x.Bolsao).HasForeignKey(x => x.BolsaoId);
        }
    }
}
