using Common.Events;
using JobPostings.Domain.Aggregates.Companies;
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
        var newCompany = new Company(companyCreated.Name, companyCreated.Identifier);
        await _companyRepository.Add(newCompany);
    }
}