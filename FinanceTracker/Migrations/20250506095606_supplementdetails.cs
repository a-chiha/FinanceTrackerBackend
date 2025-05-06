using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceTracker.Migrations
{
    /// <inheritdoc />
    public partial class supplementdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplementDetails",
                columns: table => new
                {
                    Weekday = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    JobCompanyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplementDetails", x => new { x.Weekday, x.CompanyName });
                    table.ForeignKey(
                        name: "FK_SupplementDetails_Jobs_JobCompanyName_JobUserId",
                        columns: x => new { x.JobCompanyName, x.JobUserId },
                        principalTable: "Jobs",
                        principalColumns: new[] { "CompanyName", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplementDetails_JobCompanyName_JobUserId",
                table: "SupplementDetails",
                columns: new[] { "JobCompanyName", "JobUserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplementDetails");
        }
    }
}
