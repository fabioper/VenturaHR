using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Repositories;

namespace JobPostings.Infra.Persistence.Repositories;

public class ApplicantRepository : BaseRepository<Applicant>, IApplicantRepository
{
    public ApplicantRepository(JobPostingsContext context) : base(context) {}
}