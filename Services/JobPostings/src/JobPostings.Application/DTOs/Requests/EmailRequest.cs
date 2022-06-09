namespace JobPostings.Application.DTOs.Requests;

public record EmailRequest(string To, string Subject, string Body);