using ClientRegistryAPI.Models.Domain;

namespace ClientRegistryAPI.Repositories
{
    public interface ICacheRepository
    {
        #region Email
        void SaveFailedNotificedUser(User user);

        IEnumerable<User> GetFailedNotificedUsers();

        void DeleteFailedNotificedUser(User user);

        #endregion

        #region CacheUser
        Task<bool> IsUserNameOrEmailUsed(string userName, string email);

        Task<CachedUser?> GetUserByUserNameAndEmail(string userName, string email);

        IEnumerable<CachedUser?> GetAllUser();

        Task<IEnumerable<CachedUser?>> GetAllUserAsync();

        Task<CachedUser?> GetUserAsync(string id);

        Task<CachedUser?> AddUserAsync(CachedUser user);

        Task<CachedUser?> DeleteUserAsync(string id);

        Task<CachedUser?> UpdateUserAsync(string id, CachedUser user);

        #endregion
    }
}
