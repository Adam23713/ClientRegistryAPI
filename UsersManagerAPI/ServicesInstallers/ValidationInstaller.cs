using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ClientRegistryAPI.Filters;
using ClientRegistryAPI.Requests;
using ClientRegistryAPI.Validators;

namespace ClientRegistryAPI.ServicesInstallers
{
    public class ValidationInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //Add fluent validation
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<AddUserRequest>, UserRequestValidator>();
            services.AddScoped<IValidator<UpdateUserRequest>, UserRequestValidator>();

            //Add action filter
            services.AddScoped<ValidationFilter>();
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        }
    }
}
