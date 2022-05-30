using JobPostings.Domain.Aggregates.Application;
using JobPostings.Domain.Repositories;
using ApplicationId = JobPostings.Domain.Common.ApplicationId;

namespace JobPostings.Infra.Persistence.Repositories;

public class ApplicationRepository :
    BaseRepository<Application, ApplicationId>, IJobApplicationRepository
{
    public ApplicationRepository(ModelContext context) : base(context) { }
}