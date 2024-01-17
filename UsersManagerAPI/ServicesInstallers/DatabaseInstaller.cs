
using ClientRegistryAPI.Data;
using ClientRegistryAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientRegistryAPI.ServicesInstallers
{
    public class DatabaseInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("name=DefaultConnection"));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<AuditRepository>();
        }
    }
}
