namespace ClientRegistryAPI.Models.DTO
{
    public class CachedUserDTO
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
