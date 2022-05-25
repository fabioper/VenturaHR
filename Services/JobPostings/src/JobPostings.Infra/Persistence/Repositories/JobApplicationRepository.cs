using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Repositories;

namespace JobPostings.Infra.Persistence.Repositories;

public class JobApplicationRepository :
    BaseRepository<JobApplication, JobApplicationId>, IJobApplicationRepository
{
    public JobApplicationRepository(ModelContext context) : base(context) { }
}