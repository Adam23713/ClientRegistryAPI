using ClientRegistryAPI.Models.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ClientRegistryAPI.Data
{
    public class UsersMongoDbContext
    {
        public IMongoCollection<CachedUser> UsersCollection { get; private set; }
        public IMongoCollection<User> FailedEmailCollection { get; private set; }


        public UsersMongoDbContext(IOptions<MongoDBSettings> mongoDBSettings)
        {
            if(mongoDBSettings == null)
            {
                throw new ArgumentNullException(nameof(mongoDBSettings));
            }
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI.ToString());
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            UsersCollection = database.GetCollection<CachedUser>(mongoDBSettings.Value.UserCollectionName);
            FailedEmailCollection = database.GetCollection<User>(mongoDBSettings.Value.FailedEmailCollectionName);
        }
    }
}
