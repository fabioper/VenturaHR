using JobPostings.Infra.Jobs;
using Quartz;

namespace JobPostings.Api.Common.Extensions.DependencyInjection;

public static class QuartzConfigurationExtensions
{
    public static void AddCronJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            var expiringJobsNotifierJobKey = new JobKey("expiringJobsNotifierJobKey");

            q.AddJob<ExpiringJobsNotifierJob>(opts => opts.WithIdentity(expiringJobsNotifierJobKey));

            q.AddTrigger(opts =>
            {
                opts.ForJob(expiringJobsNotifierJobKey)
                    .WithIdentity($"{nameof(expiringJobsNotifierJobKey)}-trigger")
                    .WithCronSchedule(CronScheduleBuilder.DailyAtHourAndMinute(20, 36));
            });
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }
}