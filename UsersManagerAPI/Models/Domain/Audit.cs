namespace ClientRegistryAPI.Models.Domain
{
    public class Audit
    {
        public int Id { get; set; }
        public string Event { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public DateTime Timestamp { get; private set; }

        public Audit(string @event, string detail)
        {
            Event = @event;
            Detail = detail;
            Timestamp = DateTime.Now;
        }
    }
}
