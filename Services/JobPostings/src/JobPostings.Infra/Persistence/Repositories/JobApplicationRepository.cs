using JobPostings.Domain.Aggregates.Application;
using JobPostings.Domain.Aggregates.Applications;
using JobPostings.Domain.Repositories;
using ApplicationId = JobPostings.Domain.Common.ApplicationId;

namespace JobPostings.Infra.Persistence.Repositories;

public class JobApplicationRepository :
    BaseRepository<JobApplication, ApplicationId>, IJobApplicationRepository
{
    public JobApplicationRepository(ModelContext context) : base(context) { }
}