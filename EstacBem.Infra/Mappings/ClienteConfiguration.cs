using EstacBem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstacBem.Infra.Mappings
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.SobreNome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CPF)
                .IsRequired()
                .HasMaxLength(14);

            //Relacionamentos
            builder.HasMany(x => x.Veiculos).WithOne(x => x.Cliente).HasForeignKey(x => x.ClienteId);
        }
    }
}
