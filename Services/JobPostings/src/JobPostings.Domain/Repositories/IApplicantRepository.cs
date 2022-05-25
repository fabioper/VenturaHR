using Common.Abstractions;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Repositories;

public interface IApplicantRepository : IBaseRepository<Applicant, ApplicantId>
{
}