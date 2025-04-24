using Microsoft.EntityFrameworkCore;

namespace NovaExpediente.Persistence
{
    public class NovaExpedienteDbContextFactory : DesignTimeDbContextFactoryBase<NovaExpedienteDbContext>
    {
        protected override NovaExpedienteDbContext CreateNewInstance(DbContextOptions<NovaExpedienteDbContext> options)
        {
            return new NovaExpedienteDbContext(options);
        }
    }
}
