

using ClientRegistryAPI.Models.Domain;

namespace ClientRegistryAPI.Models.Domain
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User : BaseEntity
    {
        private string name;
        private string email;

        public string Name { get { return name; } set { name = value; RefreshLastModifyDate(); } }
        public string Email { get { return email; } set { email = value; RefreshLastModifyDate(); } }

        public User(string name, string email) 
        {
            this.name = name;
            this.email = email;
        }
    }
}
