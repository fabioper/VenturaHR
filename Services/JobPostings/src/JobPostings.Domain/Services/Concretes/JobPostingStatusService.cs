using JobPostings.CrossCutting.Resources;
using JobPostings.CrossCutting.Services.Email;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Repositories;
using JobPostings.Domain.Services.Contracts;
using JobPostings.Domain.Specifications;

namespace JobPostings.Domain.Services.Concretes;

public class JobPostingStatusService : IJobPostingStatusService
{
    private const int ExpirationLimitInDays = 2;
    private readonly IJobPostingRepository _jobPostingRepository;
    private readonly IEmailService _emailService;

    public JobPostingStatusService(
        IJobPostingRepository jobPostingRepository,
        IEmailService emailService)
    {
        _jobPostingRepository = jobPostingRepository;
        _emailService = emailService;
    }

    public async Task NotifyCompaniesOfExpiringJobs()
    {
        var aboutToExpireSpec = new AboutToExpireJobPostingsSpecification();
        var expiringJobs = (await _jobPostingRepository.GetAllBy(aboutToExpireSpec)).ToList();

        if (!expiringJobs.Any())
            return;

        foreach (var expiringJob in expiringJobs)
        {
            await _emailService.SendMail(expiringJob.Company.Email, EmailResources.ExpiringJobTitle, string.Format(
                EmailResources.ExpiringJobBody, expiringJob.Title,
                expiringJob.ExpireAt.ToShortDateString()));
        }
    }

    public async Task UpdateStatusOfExpiredJobs()
    {
        var expiredSpecification = new ExpiredJobPostingsSpecification();
        var expiredJobs = await _jobPostingRepository.GetAllBy(expiredSpecification);
        foreach (var expiredJob in expiredJobs)
        {
            expiredJob.UpdateStatus(JobPostingStatus.Expired);
            await _jobPostingRepository.Update(expiredJob);
            await _emailService.SendMail(
                expiredJob.Company.Email, EmailResources.ExpiredJobTitle,
                string.Format(EmailResources.ExpiredJobBody, expiredJob.Title));
        }
    }

    public async Task UpdateStatusOfJobsExpiredMoreThanLimit()
    {
        var expiredMoreThanDaysSpecification = new ExpiredMoreThanDaysSpecification(ExpirationLimitInDays);
        var longExpiredJobPosting = await _jobPostingRepository.GetAllBy(expiredMoreThanDaysSpecification);

        foreach (var expiredJob in longExpiredJobPosting)
        {
            expiredJob.UpdateStatus(JobPostingStatus.Closed);
            await _emailService.SendMail(
                expiredJob.Company.Email, EmailResources.ClosedJobTitle,
                string.Format(EmailResources.ClosedJobBody, expiredJob.Title, ExpirationLimitInDays));
        }
    }
}