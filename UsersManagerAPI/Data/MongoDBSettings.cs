namespace ClientRegistryAPI.Data
{
    public class MongoDBSettings
    {
        public Uri ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
