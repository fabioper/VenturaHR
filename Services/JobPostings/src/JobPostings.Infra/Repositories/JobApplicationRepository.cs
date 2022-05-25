using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Repositories;
using JobPostings.Infra.Data;

namespace JobPostings.Infra.Repositories;

public class JobApplicationRepository :
    BaseRepository<JobApplication, JobApplicationId>, IJobApplicationRepository
{
    public JobApplicationRepository(ModelContext context) : base(context) { }
}