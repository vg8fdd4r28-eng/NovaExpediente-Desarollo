using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Persistence.Configurations
{
    class FormacionAcademicaConfiguration : IEntityTypeConfiguration<FormacionAcademica>
    {
        public void Configure(EntityTypeBuilder<FormacionAcademica> builder)
        {

            builder.Property(e => e.IDFORMACIONACADEMICA).HasColumnName("IDFORMACIONACADEMICA");

            builder.Property(e => e.NOMBRE)
                .IsRequired()
                .HasMaxLength(100);

            //builder.Property(e => e.PRIMERAPELLIDO).HasColumnType("ntext");
          //  builder.Property(e => e.SEGUNDOAPELLIDO).HasColumnType("ntext");

            // builder.Property(e => e.Orden).HasColumnType("int");
        }


    }
}
