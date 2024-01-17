using ClientRegistryAPI.Data;
using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Repositories;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ClientRegistryAPI.Repositories
{
    /// <summary>
    /// Database repository for MongoDB
    /// </summary>
    public class MongoDBUserRepository : ICacheRepository
    {
        private readonly UsersMongoDbContext usersMongoDbContext;

        public MongoDBUserRepository(UsersMongoDbContext usersMongoDbContext)
        {
            this.usersMongoDbContext = usersMongoDbContext;
        }

        public async Task<CachedUser?> GetUserByUserNameAndEmail(string userName, string email)
        {
            var filter = Builders<CachedUser>.Filter.Or(
               Builders<CachedUser>.Filter.Eq(u => u.Name, userName),
               Builders<CachedUser>.Filter.Eq(u => u.Email, email)
           );

            var users = await usersMongoDbContext.usersCollection.FindAsync(filter);
            return await users.FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserNameOrEmailUsed(string userName, string email)
        {
            var user = await GetUserByUserNameAndEmail(userName, email);
            return user != null;
        }

        public IEnumerable<CachedUser?> GetAllUser()
        {
            return usersMongoDbContext.usersCollection.Find(new BsonDocument()).ToList();
        }

        public async Task<IEnumerable<CachedUser?>> GetAllUserAsync()
        {
            return await usersMongoDbContext.usersCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<CachedUser?> GetUserAsync(string id)
        {
            var filter = Builders<CachedUser>.Filter.Eq(u => u.Id, id);
            return await usersMongoDbContext.usersCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<CachedUser?> AddUserAsync(CachedUser user)
        {
            await usersMongoDbContext.usersCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<CachedUser?> DeleteUserAsync(string id)
        {
            var existingUser = await GetUserAsync(id);
            if (existingUser == null)
            {
                return null;
            }

            // Delete the existing user
            FilterDefinition<CachedUser> filter = Builders<CachedUser>.Filter.Eq("Id", id);
            await usersMongoDbContext.usersCollection.DeleteOneAsync(filter);
            return existingUser;
        }

        public async Task<CachedUser?> UpdateUserAsync(string id, CachedUser user)
        {
            // Create the filter and update definitions
            var filter = Builders<CachedUser>.Filter.Eq(u => u.Id, id);
            var update = Builders<CachedUser>.Update
                .Set(u => u.Name, user.Name)
                .Set(u => u.Email, user.Email);

            // Perform the update and check the result
            var updateResult = await usersMongoDbContext.usersCollection.UpdateOneAsync(filter, update);

            // Check if the update was successful or nothing updated
            if (updateResult.ModifiedCount == 1 || updateResult.MatchedCount == 1)
            {
                // If successful, return the updated user
                user.Id = id; //User id is null
                return user;
            }
            else
            {
                // If not successful, return null
                return null;
            }
        }
    }
}