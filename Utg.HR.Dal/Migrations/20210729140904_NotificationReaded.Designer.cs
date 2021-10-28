﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Utg.HR.Dal.SqlContext;

namespace Utg.HR.Dal.Migrations
{
    [DbContext(typeof(UtgContext))]
    [Migration("20210729140904_NotificationReaded")]
    partial class NotificationReaded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Utg.HR.Common.Models.Domain.HrRequest", b =>
                {
                    b.Property<int>("HrRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("HrRequestId");

                    b.ToTable("HrRequests");
                });

            modelBuilder.Entity("Utg.HR.Common.Models.Domain.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("NotificationType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Readed")
                        .HasColumnType("boolean");

                    b.Property<int>("VacationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VacationId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Utg.HR.Common.Models.Domain.Vacation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("integer");

                    b.Property<string>("VacationType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Vacations");
                });

            modelBuilder.Entity("Utg.HR.Common.Models.Domain.VacationRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ChangeVacationId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("integer");

                    b.Property<string>("VacationRequestState")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VacationType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("VacationRequests");
                });

            modelBuilder.Entity("Utg.HR.Common.Models.Domain.VacationRequestHistoryChange", b =>
                {
                    b.Property<int>("VacationRequestHistoryChangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("RequestId")
                        .HasColumnType("integer");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("VacationRequestHistoryChangeId");

                    b.HasIndex("RequestId");

                    b.ToTable("VacationRequestHistoryChanges");
                });

            modelBuilder.Entity("Utg.HR.Common.Models.Domain.Notification", b =>
                {
                    b.HasOne("Utg.HR.Common.Models.Domain.Vacation", "Vacation")
                        .WithMany()
                        .HasForeignKey("VacationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Utg.HR.Common.Models.Domain.VacationRequestHistoryChange", b =>
                {
                    b.HasOne("Utg.HR.Common.Models.Domain.VacationRequest", "VacationRequest")
                        .WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
