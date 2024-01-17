using Swashbuckle.AspNetCore.Filters;
using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Requests;

namespace ClientRegistryAPI.SwaggerExamples.Requests
{
    /// <summary>
    /// Example for update a user (to swagger GUI)
    /// </summary>
    public class UpdateUserRequestExample : IExamplesProvider<UpdateUserRequest>
    {
        public UpdateUserRequest GetExamples()
        {
            return new UpdateUserRequest
            {
                Name = "name",
                Email = "example.email@example.hu"           
            };
        }
    }
}
