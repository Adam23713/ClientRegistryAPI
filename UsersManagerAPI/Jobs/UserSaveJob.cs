using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Repositories;
using ClientRegistryAPI.Services;
using Quartz;

namespace ClientRegistryAPI.Jobs
{
    public class UserSaveJob : IJob
    {
        private readonly ICacheRepository cacheRepository;
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;
        private readonly ILogger<UserSaveJob> logger;

        public UserSaveJob(ICacheRepository cacheRepository, IUserRepository userRepository, IEmailService emailService, ILogger<UserSaveJob> logger)
        {
            this.cacheRepository = cacheRepository;
            this.userRepository = userRepository;
            this.emailService = emailService;
            this.logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            IList<User> savedUsers = new List<User>();
            foreach (var cachedUser in cacheRepository.GetAllUser())
            {
                try
                {
                    if (cachedUser == null || cachedUser.Id == null)
                    {
                        logger.LogError("Cached user is null");
                        continue;
                    }

                    var user = new User(cachedUser.Name, cachedUser.Email);
                    var savedUser = userRepository.AddUserAsync(user);

                    savedUser.Wait();
                    if (savedUser.Result != null)
                    {
                        savedUsers.Add(savedUser.Result);
                        cacheRepository.DeleteUserAsync(cachedUser.Id).Wait();
                        logger.LogInformation($"Saved user ({savedUser.Result.Id}) to the database and deleted from the cache ({cachedUser.Id})");
                    }
                    else
                    {
                        logger.LogError($"Failed to add cached user to the database: {cachedUser.Id}");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"An error occurred while processing a cached user: {ex.Message}");
                }
            }

            //Send emails
            emailService.SendActivationSuccessEmail(savedUsers);

            return Task.CompletedTask;
        }
    }
}