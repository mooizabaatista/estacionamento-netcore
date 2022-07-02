using EstacBem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstacBem.Infra.Mappings
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(X => X.Tipo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(X => X.Nome)
                .IsRequired()
                .HasMaxLength(50);


            builder.HasData(
                new Status
                {
                    Id = 1,
                    Tipo = "Bolsão",
                    Nome = "Livre"
                }, new
                {
                    Id = 2,
                    Tipo = "Bolsão",
                    Nome = "Lotado"
                }, new
                {
                    Id = 3,
                    Tipo = "Pagamento",
                    Nome = "Pendente"
                }, new
                {
                    Id = 4,
                    Tipo = "Pagamento",
                    Nome = "Pago"
                }
                );
        }
    }
}
