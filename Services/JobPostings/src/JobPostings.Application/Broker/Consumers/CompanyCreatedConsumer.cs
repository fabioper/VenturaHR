using Common.Abstractions;
using Common.Events;
using JobPostings.Domain.CompanyAggregate;
using MassTransit;

namespace JobPostings.Application.Broker.Consumers;

public class CompanyCreatedConsumer : IConsumer<CompanyCreatedEvent>
{
    private readonly IRepository<Company> _companiesRepository;

    public CompanyCreatedConsumer(IRepository<Company> companiesRepository) =>
        _companiesRepository = companiesRepository;

    public async Task Consume(ConsumeContext<CompanyCreatedEvent> context)
    {
        var newCompany = context.Message;
        var company = new Company(newCompany.Identifier, newCompany.Name);
        await _companiesRepository.Add(company);
    }
}