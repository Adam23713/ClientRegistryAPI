using Swashbuckle.AspNetCore.Filters;
using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Requests;

namespace ClientRegistryAPI.SwaggerExamples.Requests
{
    /// <summary>
    /// Example for create a user (to swagger GUI)
    /// </summary>
    public class CreateUserRequestExample : IExamplesProvider<AddUserRequest>
    {
        public AddUserRequest GetExamples()
        {
            return new AddUserRequest
            {
                Name = "name",
                Email = "example.email@example.hu"
            };
        }
    }
}
