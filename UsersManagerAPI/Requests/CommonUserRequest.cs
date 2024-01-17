using ClientRegistryAPI.Models.Domain;

namespace ClientRegistryAPI.Requests
{
    /// <summary>
    /// Contains user properties for the request
    /// </summary>
    public class CommonUserRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
