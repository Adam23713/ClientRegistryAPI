using ClientRegistryAPI.Data;
using ClientRegistryAPI.Models.Domain;

namespace ClientRegistryAPI.Repositories
{
    /// <summary>
    /// Database repository interface
    /// </summary>
    public interface IUserRepository
    {
        Task<bool> IsUserNameOrEmailUsed(string userName, string email);

        Task<User?> GetUserByUserNameAndEmail(string userName, string email);

        Task<IEnumerable<User?>> GetAllUserAsync();

        Task<User?> GetUserAsync(int id);

        Task<User?> AddUserAsync(User user);

        Task<User?> DeleteUserAsync(int id);

        Task<User?> UpdateUserAsync(int id, User user);
    }
}
