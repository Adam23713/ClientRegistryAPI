using ClientRegistryAPI.Data;
using ClientRegistryAPI.Repositories;

namespace ClientRegistryAPI.ServicesInstallers
{
    public class MongoDBInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            if(configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            services.Configure<MongoDBSettings>(configuration.GetSection("LocalMongoDB"/*"MongoDB"*/));
            services.AddSingleton<UsersMongoDbContext>();
            services.AddScoped<ICacheRepository, MongoDBUserRepository>();
        }
    }
}
