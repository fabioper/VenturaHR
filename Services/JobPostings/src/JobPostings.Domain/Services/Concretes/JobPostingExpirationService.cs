using JobPostings.CrossCutting.Services.Email;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Repositories;
using JobPostings.Domain.Services.Contracts;
using JobPostings.Domain.Specifications;

namespace JobPostings.Domain.Services.Concretes;

public class JobPostingExpirationService : IJobPostingExpirationService
{
    private const int ExpirationLimitInDays = 2;
    private readonly IJobPostingRepository _jobPostingRepository;
    private readonly IEmailService _emailService;

    public JobPostingExpirationService(
        IJobPostingRepository jobPostingRepository,
        IEmailService emailService)
    {
        _jobPostingRepository = jobPostingRepository;
        _emailService = emailService;
    }

    public async Task NotifyCompaniesOfExpiringJobs()
    {
        var expiringJobs = await GetExpiringJobs();

        if (!expiringJobs.Any())
            return;

        foreach (var expiringJob in expiringJobs)
        {
            const string title = "Vaga prestes à expirar";
            var body =
                $"A vaga {expiringJob.Title} está prevista para expirar em {expiringJob.ExpireAt.ToShortDateString()}.";
            await _emailService.SendMail(expiringJob.Company.Email, title, body);
        }
    }

    public async Task UpdateStatusOfExpiredJobs()
    {
        var expiredSpecification = new ExpiredJobPostingsSpecification();
        var expiredJobs = await _jobPostingRepository.GetAllBy(expiredSpecification);
        foreach (var expiredJob in expiredJobs)
        {
            expiredJob.UpdateStatus(JobPostingStatus.Expired);
            await _emailService.SendMail(
                expiredJob.Company.Email, "Vaga expirada", $"A vaga {expiredJob.Title} expirou.");
        }
    }

    public async Task UpdateStatusOfJobsExpiredMoreThanLimit()
    {
        var expiredMoreThanDays = new ExpiredMoreThanDaysSpecification(ExpirationLimitInDays);
        var longExpiredJobPosting = await _jobPostingRepository.GetAllBy(expiredMoreThanDays);

        foreach (var expiredJob in longExpiredJobPosting)
        {
            expiredJob.UpdateStatus(JobPostingStatus.Closed);
            await _emailService.SendMail(
                expiredJob.Company.Email, "Vaga fechada",
                $"A vaga {expiredJob.Title} expirou há mais de {ExpirationLimitInDays} dias e não foi renovada, portanto foi fechada.");
        }
    }

    private async Task<List<JobPosting>> GetExpiringJobs()
    {
        var aboutToExpireSpec = new ExpiringJobPostingsSpecification();
        var expiringJobs = (await _jobPostingRepository.GetAllBy(aboutToExpireSpec)).ToList();
        return expiringJobs;
    }
}