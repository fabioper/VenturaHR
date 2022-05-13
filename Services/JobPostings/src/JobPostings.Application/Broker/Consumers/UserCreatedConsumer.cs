using Common;
using Common.Events.Models;
using JobPostings.Domain.CompanyAggregate;
using MassTransit;

namespace JobPostings.Application.Broker.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    private readonly IRepository<Company> _companiesRepository;

    public UserCreatedConsumer(IRepository<Company> companiesRepository) =>
        _companiesRepository = companiesRepository;

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        var companyUser = context.Message;
        var company = new Company(companyUser.Name, companyUser.ExternalId);
        await _companiesRepository.Add(company);
    }
}