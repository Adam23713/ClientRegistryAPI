namespace ClientRegistryAPI.Data
{
    public class MongoDBSettings
    {
        public Uri ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
        public string FailedEmailCollectionName { get; set; } = null!;
    }
}
