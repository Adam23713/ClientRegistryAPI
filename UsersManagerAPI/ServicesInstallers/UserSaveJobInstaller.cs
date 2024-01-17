
using ClientRegistryAPI.Jobs;
using Quartz;

namespace ClientRegistryAPI.ServicesInstallers
{
    public class UserSaveJobInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(q =>
            {
                // Job to save users at 10 PM
                var userSaveJobKey = new JobKey("userSaveJob");
                q.AddJob<UserSaveJob>(j => j.WithIdentity(userSaveJobKey));
                q.AddTrigger(t => t
                    .ForJob(userSaveJobKey)
                    .WithIdentity("userSaveTrigger")
                    /*.WithDailyTimeIntervalSchedule(s => s
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(22, 0))
                        .EndingDailyAfterCount(1)
                        .WithIntervalInHours(24)));*/

                .WithDailyTimeIntervalSchedule(s => s
        .WithIntervalInMinutes(1) // Run every minute
        .OnEveryDay()
        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)) // Start at midnight
    ));
        });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
