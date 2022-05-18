using Microsoft.AspNetCore.Mvc;
using Users.Api.DTOs.Requests;
using Users.Api.Services.Contracts;

namespace Users.Api.Controllers;

[ApiController]
[Route("/companies")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompaniesController(ICompanyService companyService) =>
        _companyService = companyService;

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyProfileRequest request)
    {
        await _companyService.CreateCompanyProfile(request);
        return Ok();
    }

    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetByExternalId([FromRoute] string companyId)
    {
        var applicant = await _companyService.FindCompanyByExternalId(companyId);
        return Ok(applicant);
    }
}