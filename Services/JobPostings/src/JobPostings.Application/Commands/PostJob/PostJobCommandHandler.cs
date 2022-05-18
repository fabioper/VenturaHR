using JobPostings.Domain.JobPostingAggregate;
using MediatR;

namespace JobPostings.Application.Commands.PostJob;

public class PostJobCommandHandler : IRequestHandler<PostJobCommand>
{
    private readonly IJobPostingRepository _jobPostingsRepository;

    public PostJobCommandHandler(IJobPostingRepository jobPostingsRepository) =>
        _jobPostingsRepository = jobPostingsRepository;

    public async Task<Unit> Handle(PostJobCommand request, CancellationToken cancellationToken)
    {
        var newJob = new JobPosting(request.Role,
            request.Description,
            request.Location,
            request.Salary,
            request.ExpirationDate,
            request.CompanyId);

        await _jobPostingsRepository.Add(newJob);
        return Unit.Value;
    }
}