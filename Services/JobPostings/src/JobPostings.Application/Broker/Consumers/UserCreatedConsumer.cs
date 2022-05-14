using Common.Abstractions;
using Common.Events;
using JobPostings.Domain.CompanyAggregate;
using MassTransit;

namespace JobPostings.Application.Broker.Consumers;

public class UserCreatedConsumer : IConsumer<CompanyCreatedEvent>
{
    private readonly IRepository<Company> _companiesRepository;

    public UserCreatedConsumer(IRepository<Company> companiesRepository) =>
        _companiesRepository = companiesRepository;

    public async Task Consume(ConsumeContext<CompanyCreatedEvent> context)
    {
        var companyUser = context.Message;
        var company = new Company(companyUser.Name, companyUser.Identifier);
        await _companiesRepository.Add(company);
    }
}