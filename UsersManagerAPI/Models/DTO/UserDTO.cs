using ClientRegistryAPI.Models.Domain;

namespace ClientRegistryAPI.Models.DTO
{
    /// <summary>
    /// The user dto model.
    /// </summary>
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
