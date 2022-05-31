﻿// <auto-generated />
using System;
using JobPostings.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobPostings.Infra.Persistence.Migrations
{
    [DbContext(typeof(ModelContext))]
    [Migration("20220531190144_RenameCriteriaAnswer")]
    partial class RenameCriteriaAnswer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JobPostings.Domain.Aggregates.Applicants.Applicant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Applicants", (string)null);
                });

            modelBuilder.Entity("JobPostings.Domain.Aggregates.Companies.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("JobPostings.Domain.Aggregates.Criterias.Criteria", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.Property<Guid>("_jobPostingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("_jobPostingId");

                    b.ToTable("Criterias", (string)null);
                });

            modelBuilder.Entity("JobPostings.Domain.Aggregates.JobApplications.CriteriaAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.Property<Guid>("_applicationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("_criteriaId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("_applicationId");

                    b.HasIndex("_criteriaId");

                    b.ToTable("CriteriaAnswers", (string)null);
                });

            modelBuilder.Entity("JobPostings.Domain.Aggregates.JobApplications.JobApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AppliedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("_applicantId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("_jobPostingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("_applicantId");

                    b.HasIndex("_jobPostingId");

                    b.ToTable("Applications", (string)null);
                });

            modelBuilder.Entity("JobPostings.Domain.Aggregates.JobPostings.JobPosting", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpireAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("_companyId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("_companyId");

                    b.ToTable("JobPostings", (string)null);
                });

            modelBuilder.Entity("JobPostings.Domain.Aggregates.Criterias.Criteria", b =>
                {
                    b.HasOne("JobPostings.Domain.Aggregates.JobPostings.JobPosting", null)
                        .WithMany("Criterias")
                        .HasForeignKey("_jobPostingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobPostings.Domain.Aggregates.JobApplications.CriteriaAnswer", b =>
                {
                    b.HasOne("JobPostings.Domain.Aggregates.JobApplications.JobApplication", null)
                        .WithMany("CriteriasFullfillments")
                        .HasForeignKey("_applicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobPostings.Domain.Aggregates.Criterias.Criteria", "Criteria")
                        .WithMany()
                        .HasForeignKey("_criteriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Criteria");
                });

            modelBuilder.Entity("JobPostings.Domain.Aggregates.JobApplications.JobApplication", b =>
                {
                    b.HasOne("JobPostings.Domain.Aggregates.Applicants.Applicant", "Applicant")
                        .WithMany()
                        .HasForeignKey("_applicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobPostings.Domain.Aggregates.JobPostings.JobPosting", "JobPosting")
                        .WithMany()
                        .HasForeignKey("_jobPostingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("JobPosting");
                });

            modelBuilder.Entity("JobPostings.Domain.Aggregates.JobPostings.JobPosting", b =>
                {
                    b.HasOne("JobPostings.Domain.Aggregates.Companies.Company", "Company")
                        .WithMany()
                        .HasForeignKey("_companyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("JobPostings.Domain.Aggregates.JobPostings.Salary", "Salary", b1 =>
                        {
                            b1.Property<Guid>("JobPostingId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("Compensation");

                            b1.HasKey("JobPostingId");

                            b1.ToTable("JobPostings");

                            b1.WithOwner()
                                .HasForeignKey("JobPostingId");
                        });

                    b.Navigation("Company");

                    b.Navigation("Salary");
                });

            modelBuilder.Entity("JobPostings.Domain.Aggregates.JobApplications.JobApplication", b =>
                {
                    b.Navigation("CriteriasFullfillments");
                });

            modelBuilder.Entity("JobPostings.Domain.Aggregates.JobPostings.JobPosting", b =>
                {
                    b.Navigation("Criterias");
                });
#pragma warning restore 612, 618
        }
    }
}
