using Common.Abstractions;
using JobPostings.Domain.Aggregates.Applicants;

namespace JobPostings.Domain.Repositories;

public interface IApplicantRepository : IBaseRepository<Applicant>
{
}