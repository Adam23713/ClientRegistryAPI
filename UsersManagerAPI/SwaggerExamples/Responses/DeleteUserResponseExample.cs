using Swashbuckle.AspNetCore.Filters;
using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Models.DTO;
using ClientRegistryAPI.Responses;

namespace ClientRegistryAPI.SwaggerExamples.Responses
{
    public class DeleteUserResponseExample : IExamplesProvider<DeleteUserResponse>
    {
        public DeleteUserResponse GetExamples()
        {
            return new DeleteUserResponse(new UserDTO
            {
                Id = 1,
                Name = "name",
                Email = "example1.email@example.hu",
            });
        }
    }
}
