using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Repositories;
using JobPostings.Infra.Data;

namespace JobPostings.Infra.Repositories;

public class ApplicantRepository : BaseRepository<Applicant, ApplicantId>, IApplicantRepository
{
    public ApplicantRepository(ModelContext context) : base(context) {}
}