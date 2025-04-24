using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Persistence.Configurations
{
    public class ClasificadorConfiguration : IEntityTypeConfiguration<Clasificador>
    {
        public void Configure(EntityTypeBuilder<Clasificador> builder)
        {

            builder.Property(e => e.IdClasificador).HasColumnName("IdClasificador");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Descripcion).HasColumnType("ntext");

        }
    }
}
