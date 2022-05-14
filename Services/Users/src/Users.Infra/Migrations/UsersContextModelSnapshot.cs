﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Users.Infra.Data;

#nullable disable

namespace Users.Infra.Migrations
{
    [DbContext(typeof(UsersContext))]
    partial class UsersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Users.Infra.Data.Models.Entities.Applicant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Applicants", (string)null);
                });

            modelBuilder.Entity("Users.Infra.Data.Models.Entities.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("Users.Infra.Data.Models.Entities.Applicant", b =>
                {
                    b.OwnsOne("Users.Infra.Data.Models.ValueObjects.ExternalId", "ExternalId", b1 =>
                        {
                            b1.Property<long>("ApplicantId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("ExternalId");

                            b1.HasKey("ApplicantId");

                            b1.ToTable("Applicants");

                            b1.WithOwner()
                                .HasForeignKey("ApplicantId");
                        });

                    b.Navigation("ExternalId");
                });

            modelBuilder.Entity("Users.Infra.Data.Models.Entities.Company", b =>
                {
                    b.OwnsOne("Users.Infra.Data.Models.ValueObjects.ExternalId", "ExternalId", b1 =>
                        {
                            b1.Property<long>("CompanyId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("ExternalId");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.OwnsOne("Users.Infra.Data.Models.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<long>("CompanyId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.OwnsOne("Users.Infra.Data.Models.ValueObjects.Registration", "Registration", b1 =>
                        {
                            b1.Property<long>("CompanyId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Number")
                                .HasColumnType("text")
                                .HasColumnName("Registration");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.Navigation("ExternalId");

                    b.Navigation("PhoneNumber");

                    b.Navigation("Registration");
                });
#pragma warning restore 612, 618
        }
    }
}