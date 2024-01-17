namespace ClientRegistryAPI.Services
{
    public interface IEmailService
    {
        void SendActivationSuccessEmail(ICollection<Models.Domain.User> users);
    }
}
