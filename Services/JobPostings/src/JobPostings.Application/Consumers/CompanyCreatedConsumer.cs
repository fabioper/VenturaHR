using Common.Events;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Repositories;
using MassTransit;

namespace JobPostings.Application.Consumers;

public class CompanyCreatedConsumer : IConsumer<CompanyCreatedEvent>
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyCreatedConsumer(ICompanyRepository companyRepository) =>
        _companyRepository = companyRepository;

    public async Task Consume(ConsumeContext<CompanyCreatedEvent> context)
    {
        var companyCreated = context.Message;
        var companyId = Guid.Parse(companyCreated.Identifier);
        var newCompany = new Company(
            companyId, companyCreated.Name, companyCreated.Email, companyCreated.PhoneNumber);
        await _companyRepository.Add(newCompany);
    }
}