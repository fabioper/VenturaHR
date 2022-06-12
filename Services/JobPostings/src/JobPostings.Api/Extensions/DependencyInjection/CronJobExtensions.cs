using JobPostings.Infra.Jobs;
using Quartz;

namespace JobPostings.Api.Extensions.DependencyInjection;

public static class CronJobExtensions
{
    public static void AddCronJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            var expiringJobsNotifierJobKey = new JobKey("expiringJobsNotifierJobKey");

            q.AddJob<ExpiringJobsNotifierJob>(opts
                => opts.WithIdentity(expiringJobsNotifierJobKey));

            var cronSchedule = CronScheduleBuilder.DailyAtHourAndMinute(0, 0);
            q.AddTrigger(TriggerConfig(expiringJobsNotifierJobKey, cronSchedule));
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }

    private static Action<ITriggerConfigurator> TriggerConfig(JobKey key, CronScheduleBuilder cronSchedule)
    {
        return opts => opts.ForJob(key)
            .WithIdentity($"{nameof(key)}-trigger")
            .WithCronSchedule(cronSchedule);
    }
}