﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchedulerPro.DAL;

#nullable disable

namespace SchedulerPro.DAL.Migrations
{
    [DbContext(typeof(SchedulerProContext))]
    [Migration("20240408232218_initialCreate")]
    partial class initialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MeetingUser", b =>
                {
                    b.Property<Guid>("AttendeesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MeetingsId")
                        .HasColumnType("uuid");

                    b.HasKey("AttendeesId", "MeetingsId");

                    b.HasIndex("MeetingsId");

                    b.ToTable("MeetingUser");
                });

            modelBuilder.Entity("SchedulerPro.DAL.Models.Meeting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("SchedulerPro.DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("TimeZoneId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cd71caec-e139-4506-bf76-c30ec2c04557"),
                            Email = "niall.lewin@hotmail.com",
                            FirstName = "Niall",
                            LastName = "Lewin",
                            PasswordHash = "$2a$11$cOVok6A9eU7OrbZeaCIn3.mFWVDb6Oiid8mvFLERYrKOqAQlvxhQ6",
                            TimeZoneId = "Europe/London"
                        },
                        new
                        {
                            Id = new Guid("dcfe90d1-a186-45e3-bd3f-94e025687468"),
                            Email = "john.davis@hotmail.com",
                            FirstName = "John",
                            LastName = "Davis",
                            PasswordHash = "$2a$11$5yAQFOYr6HPsScEKG.D.GeRBCL5VP9DwqcT5ANo1vJe5d43YQYeqy",
                            TimeZoneId = "Europe/Berlin"
                        },
                        new
                        {
                            Id = new Guid("2a3c9cff-7e20-48e5-b57e-0e992d989f15"),
                            Email = "kat.jones@hotmail.com",
                            FirstName = "Kat",
                            LastName = "Jones",
                            PasswordHash = "$2a$11$Ip6etqgnlEe/YeSHscMCmuy73B9Zj4rJFM3v5vCDbYHqdyvEHETcy",
                            TimeZoneId = "Asia/Tokyo"
                        });
                });

            modelBuilder.Entity("MeetingUser", b =>
                {
                    b.HasOne("SchedulerPro.DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("AttendeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchedulerPro.DAL.Models.Meeting", null)
                        .WithMany()
                        .HasForeignKey("MeetingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
