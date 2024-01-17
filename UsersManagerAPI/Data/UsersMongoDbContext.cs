using ClientRegistryAPI.Models.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ClientRegistryAPI.Data
{
    public class UsersMongoDbContext
    {
        public IMongoCollection<CachedUser> usersCollection { get; private set; }


        public UsersMongoDbContext(IOptions<MongoDBSettings> mongoDBSettings)
        {
            if(mongoDBSettings == null)
            {
                throw new ArgumentNullException(nameof(mongoDBSettings));
            }
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI.ToString());
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            usersCollection = database.GetCollection<CachedUser>(mongoDBSettings.Value.CollectionName);
        }
    }
}
