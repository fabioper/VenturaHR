using Microsoft.AspNetCore.Mvc;

namespace JobPostings.Api.Controllers;

[ApiController, Route("job-postings")]
public class JobPostingsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PublishJobPosting() => Ok();
}