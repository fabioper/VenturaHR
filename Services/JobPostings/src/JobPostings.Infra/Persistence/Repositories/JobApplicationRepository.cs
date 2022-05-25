using JobPostings.Domain.Aggregates.Application;
using JobPostings.Domain.Repositories;
using ApplicationId = JobPostings.Domain.Aggregates.Application.ApplicationId;

namespace JobPostings.Infra.Persistence.Repositories;

public class JobApplicationRepository :
    BaseRepository<Application, ApplicationId>, IJobApplicationRepository
{
    public JobApplicationRepository(ModelContext context) : base(context) { }
}