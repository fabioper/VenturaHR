using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Api.Models.Entities;

namespace Users.Api.Data.Configurations;

public class ApplicantConfiguration : BaseUserConfiguration<Applicant>
{
    public override void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("Applicants");
    }
}