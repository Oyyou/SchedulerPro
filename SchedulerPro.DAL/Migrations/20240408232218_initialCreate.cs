using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchedulerPro.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    TimeZoneId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetingUser",
                columns: table => new
                {
                    AttendeesId = table.Column<Guid>(type: "uuid", nullable: false),
                    MeetingsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingUser", x => new { x.AttendeesId, x.MeetingsId });
                    table.ForeignKey(
                        name: "FK_MeetingUser_Meetings_MeetingsId",
                        column: x => x.MeetingsId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingUser_Users_AttendeesId",
                        column: x => x.AttendeesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "TimeZoneId" },
                values: new object[,]
                {
                    { new Guid("2a3c9cff-7e20-48e5-b57e-0e992d989f15"), "kat.jones@hotmail.com", "Kat", "Jones", "$2a$11$Ip6etqgnlEe/YeSHscMCmuy73B9Zj4rJFM3v5vCDbYHqdyvEHETcy", "Asia/Tokyo" },
                    { new Guid("cd71caec-e139-4506-bf76-c30ec2c04557"), "niall.lewin@hotmail.com", "Niall", "Lewin", "$2a$11$cOVok6A9eU7OrbZeaCIn3.mFWVDb6Oiid8mvFLERYrKOqAQlvxhQ6", "Europe/London" },
                    { new Guid("dcfe90d1-a186-45e3-bd3f-94e025687468"), "john.davis@hotmail.com", "John", "Davis", "$2a$11$5yAQFOYr6HPsScEKG.D.GeRBCL5VP9DwqcT5ANo1vJe5d43YQYeqy", "Europe/Berlin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingUser_MeetingsId",
                table: "MeetingUser",
                column: "MeetingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingUser");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
