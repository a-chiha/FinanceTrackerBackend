using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceTracker.Migrations
{
    /// <inheritdoc />
    public partial class _0001InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HolidayPays",
                columns: table => new
                {
                    ExperationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FirstHoliday = table.Column<DateOnly>(type: "date", nullable: false),
                    HolidayPayAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayPays", x => x.ExperationDate);
                });

            migrationBuilder.CreateTable(
                name: "Paychecks",
                columns: table => new
                {
                    SalaryPeriod = table.Column<DateOnly>(type: "date", nullable: false),
                    Tax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalarayBeforeTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HolidaySupplement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pension = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Holidaycompensation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    taxDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AMContribution = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkedHours = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paychecks", x => x.SalaryPeriod);
                });

            migrationBuilder.CreateTable(
                name: "SuPayments",
                columns: table => new
                {
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    SuAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxCard = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuPayments", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => new { x.Email, x.UserId });
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    CVR = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TaxCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmploymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => new { x.UserId, x.CVR });
                    table.ForeignKey(
                        name: "FK_Jobs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkShifts",
                columns: table => new
                {
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PaycheckSalaryPeriod = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShifts", x => new { x.StartTime, x.EndTime, x.UserId });
                    table.ForeignKey(
                        name: "FK_WorkShifts_Paychecks_PaycheckSalaryPeriod",
                        column: x => x.PaycheckSalaryPeriod,
                        principalTable: "Paychecks",
                        principalColumn: "SalaryPeriod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkShifts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "supplementPays",
                columns: table => new
                {
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CVR = table.Column<int>(type: "int", nullable: false),
                    Weekday = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JobUserId = table.Column<int>(type: "int", nullable: false),
                    JobCVR = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplementPays", x => new { x.StartTime, x.EndTime, x.CVR });
                    table.ForeignKey(
                        name: "FK_supplementPays_Jobs_JobUserId_JobCVR",
                        columns: x => new { x.JobUserId, x.JobCVR },
                        principalTable: "Jobs",
                        principalColumns: new[] { "UserId", "CVR" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_supplementPays_JobUserId_JobCVR",
                table: "supplementPays",
                columns: new[] { "JobUserId", "JobCVR" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkShifts_PaycheckSalaryPeriod",
                table: "WorkShifts",
                column: "PaycheckSalaryPeriod");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShifts_UserId",
                table: "WorkShifts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "HolidayPays");

            migrationBuilder.DropTable(
                name: "SuPayments");

            migrationBuilder.DropTable(
                name: "supplementPays");

            migrationBuilder.DropTable(
                name: "WorkShifts");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Paychecks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
