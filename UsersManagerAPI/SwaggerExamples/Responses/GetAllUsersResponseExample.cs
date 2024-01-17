using Swashbuckle.AspNetCore.Filters;
using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Models.DTO;

namespace ClientRegistryAPI.SwaggerExamples.Responses
{
    public class GetAllUsersResponseExample : IExamplesProvider<ICollection<UserDTO>>
    {
        public ICollection<UserDTO> GetExamples()
        {
            return new List<UserDTO>()
            {
                new UserDTO
                {
                    Id = 1,
                    Name = "name_001",
                    Email = "example1.email@example.hu",
                },
                new UserDTO
                {
                    Id = 2,
                    Name = "name_002",
                    Email = "example2.email@example.hu",
                }
            };
        }
    }
}
