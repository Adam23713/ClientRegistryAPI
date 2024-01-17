using Swashbuckle.AspNetCore.Filters;
using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Models.DTO;

namespace ClientRegistryAPI.SwaggerExamples.Responses
{
    public class GetSelectedUserResponseExample : IExamplesProvider<UserDTO>
    {
        public UserDTO GetExamples()
        {
            return new UserDTO
            {
                Id = 1,
                Name = "name",
                Email = "example1.email@example.hu"
            };
        }
    }
}
