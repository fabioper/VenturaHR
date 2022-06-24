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

            q.AddJob<ExpiringJobsNotifierJob>(opts => opts.WithIdentity(expiringJobsNotifierJobKey));

            q.AddTrigger(opts =>
            {
                opts.ForJob(expiringJobsNotifierJobKey)
                    .WithIdentity($"{nameof(expiringJobsNotifierJobKey)}-trigger")
                    .WithCronSchedule(CronScheduleBuilder.DailyAtHourAndMinute(0, 0));
            });

            var updateJobsStatusJobKey = new JobKey("updateJobsStatusJobKey");

            q.AddJob<UpdateJobPostingStatusJob>(opts => opts.WithIdentity(updateJobsStatusJobKey));

            q.AddTrigger(x =>
            {
                x.ForJob(updateJobsStatusJobKey)
                    .WithIdentity($"{nameof(updateJobsStatusJobKey)}-trigger")
                    .WithCalendarIntervalSchedule(scheduleBuilder => scheduleBuilder.WithIntervalInMinutes(5));
            });
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }
}