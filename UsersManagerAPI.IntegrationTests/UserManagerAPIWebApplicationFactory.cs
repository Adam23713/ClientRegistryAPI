using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Primitives;
using System.Collections;
using UsersManagerAPI.Data;
using UsersManagerAPI.Repositories;

namespace UsersManagerAPI.IntegrationTests
{
    internal class UserManagerAPIWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(MongoDBSettings));
                services.RemoveAll(typeof(UsersMongoDbContext));

                services.Configure<MongoDBSettings>(configuration.GetSection("MongoDBForTest"));
                services.AddSingleton<UsersMongoDbContext>();
            });
        }
    }
}
