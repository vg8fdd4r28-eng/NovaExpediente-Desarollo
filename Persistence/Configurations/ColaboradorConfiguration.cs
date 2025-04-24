using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Persistence.Configurations
{
    class ColaboradorConfiguration : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {

            builder.Property(e => e.IDCOLABORADOR).HasColumnName("IDCOLABORADOR");

            builder.Property(e => e.NOMBRE)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.PRIMERAPELLIDO).HasColumnType("ntext");
            builder.Property(e => e.SEGUNDOAPELLIDO).HasColumnType("ntext");

           // builder.Property(e => e.Orden).HasColumnType("int");
        }


    }
}
