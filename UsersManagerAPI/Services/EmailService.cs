using ClientRegistryAPI.Jobs;
using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Repositories;

namespace ClientRegistryAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<UserSaveJob> logger;
        private readonly ICacheRepository cacheRepository;

        public EmailService(ILogger<UserSaveJob> logger, ICacheRepository cacheRepository)
        {
            this.logger = logger;
            this.cacheRepository = cacheRepository;
        }

        private bool SendEmail(User user)
        {
            bool success = true;
            try
            {
                logger.LogInformation($"Email sended to {user.Email}");
            }
            catch (Exception ex) 
            {
                success = false;
                logger.LogError(ex.Message, ex.StackTrace);
            }
            return success;
        }

        public void SendActivationSuccessEmail(ICollection<Models.Domain.User> users)
        {
            try
            {
                var failedUsers = cacheRepository.GetFailedNotificedUsers();

                //Send email to new users
                if(users != null)
                {
                    foreach (var user in users)
                    {
                        if (!SendEmail(user))
                        {
                            //Save and try later
                            cacheRepository.SaveFailedNotificedUser(user);
                        }
                    }
                }
                
                foreach (var user in failedUsers)
                {
                    if(SendEmail(user))
                    {
                        cacheRepository.DeleteFailedNotificedUser(user);
                    }
                }
            } 
            catch (Exception ex) 
            {
                logger.LogError(ex.Message, ex.StackTrace);
            }
        }
    }
}
