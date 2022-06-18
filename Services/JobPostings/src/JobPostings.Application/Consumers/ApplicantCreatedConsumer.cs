using Common.Events;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Repositories;
using MassTransit;

namespace JobPostings.Application.Consumers;

public class ApplicantCreatedConsumer : IConsumer<ApplicantCreatedEvent>
{
    private readonly IApplicantRepository _applicantRepository;

    public ApplicantCreatedConsumer(IApplicantRepository applicantRepository)
        => _applicantRepository = applicantRepository;

    public async Task Consume(ConsumeContext<ApplicantCreatedEvent> context)
    {
        var applicantCreated = context.Message;
        var applicantIdentifier = Guid.Parse(applicantCreated.Identifier);
        var applicant = new Applicant(
            applicantIdentifier, applicantCreated.Name, applicantCreated.Email, applicantCreated.PhoneNumber);
        await _applicantRepository.Add(applicant);
    }
}