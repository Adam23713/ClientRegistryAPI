using ClientRegistryAPI.Models.DTO;
using ClientRegistryAPI.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace ClientRegistryAPI.SwaggerExamples.Responses
{
    public class AddCachedUser : IExamplesProvider<CachedUserDTO>
    {
        public CachedUserDTO GetExamples()
        {
            return new CachedUserDTO
            {
                Id = "659414c2e416febaa0bcb63e",
                Name = "name",
                Email = "example1.email@example.hu",
            };
        }
    }
}
