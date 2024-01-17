using ClientRegistryAPI.Data;
using ClientRegistryAPI.Models.Domain;

namespace ClientRegistryAPI.Repositories
{
    public class AuditRepository
    {
        private readonly ApplicationDbContext context = null!;

        public AuditRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddAuditAsync(Audit audit)
        {
            if(audit != null)
            {
                context.Audits.Add(audit);
                await context.SaveChangesAsync();
            }
        }
    }
}
