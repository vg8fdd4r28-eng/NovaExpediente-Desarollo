using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Persistence.Configurations
{
    public class GradoAcademicoConfiguration : IEntityTypeConfiguration<GradoAcademico>
    {
        public void Configure(EntityTypeBuilder<GradoAcademico> builder)
        {

            builder.Property(e => e.IdGradoAcademico).HasColumnName("IdGradoAcademico");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Descripcion).HasColumnType("ntext");

            builder.Property(e => e.Orden).HasColumnType("int");
        }
    }
}
