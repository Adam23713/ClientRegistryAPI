using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using ClientRegistryAPI.ServicesInstallers;


namespace ClientRegistryAPI.Installers
{
    public class SwaggerInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSwaggerGen();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo{Title = "Users Manager API - V1", Version = "v1"} );

                x.ExampleFilters();

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "ClientRegistryAPI.xml");
                x.IncludeXmlComments(filePath);
            });

            services.AddSwaggerExamplesFromAssemblyOf<Program>();
        }
    }
}
