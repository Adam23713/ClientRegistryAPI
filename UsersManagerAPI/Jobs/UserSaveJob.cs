using AutoMapper;
using ClientRegistryAPI.Controllers;
using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Models.DTO;
using ClientRegistryAPI.Repositories;
using Quartz;

namespace ClientRegistryAPI.Jobs
{
    public class UserSaveJob : IJob
    {
        private readonly ICacheRepository cacheRepository;
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserSaveJob> logger;


        public UserSaveJob(ICacheRepository cacheRepository, IUserRepository userRepository, ILogger<UserSaveJob> logger)
        {
            this.cacheRepository = cacheRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            foreach (var cachedUser in cacheRepository.GetAllUser())
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
                    cacheRepository.DeleteUserAsync(cachedUser.Id).Wait();
                    logger.LogInformation($"Save user ({savedUser.Id}) to database and delet from cache ({cachedUser.Id})");
                }
                else
                {
                    logger.LogError($"Failed to add cached user to database: {cachedUser.Id}");
                }

            }
            return Task.CompletedTask;
        }
    }
}
