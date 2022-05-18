using Microsoft.AspNetCore.Mvc;
using Users.Api.DTOs.Requests;
using Users.Api.Services.Contracts;

namespace Users.Api.Controllers;

[ApiController]
[Route("/applicants")]
public class ApplicantsController : ControllerBase
{
    private readonly IApplicantService _applicantService;

    public ApplicantsController(IApplicantService applicantService) =>
        _applicantService = applicantService;

    [HttpPost]
    public async Task<IActionResult> CreateApplicant([FromBody] CreateApplicantProfileRequest request)
    {
        await _applicantService.CreateApplicantProfile(request);
        return Ok();
    }

    [HttpGet("{applicantId}")]
    public async Task<IActionResult> GetByExternalId([FromRoute] string applicantId)
    {
        var applicant = await _applicantService.FindApplicantByExternalId(applicantId);
        return Ok(applicant);
    }
}