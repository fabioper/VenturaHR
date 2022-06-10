using JobPostings.Infra.Jobs;
using Quartz;

namespace JobPostings.Api.Extensions.DependencyInjection;

public static class QuartzConfigurationExtensions
{
    public static void AddCronJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            var expiringJobsNotifierJobKey = new JobKey("expiringJobsNotifierJobKey");

            q.AddJob<ExpiringJobsNotifierJob>(opts => opts.WithIdentity(expiringJobsNotifierJobKey));

            q.AddTrigger(TriggerConfig(expiringJobsNotifierJobKey, CronScheduleBuilder.DailyAtHourAndMinute(0, 0)));
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }

    private static Action<ITriggerConfigurator> TriggerConfig(JobKey key, CronScheduleBuilder cronSchedule)
    {
        return opts =>
        {
            opts.ForJob(key)
                .WithIdentity($"{nameof(key)}-trigger")
                .WithCronSchedule(cronSchedule);
        };
    }
}