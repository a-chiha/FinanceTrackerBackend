using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddedPaycheckEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "VacationPay",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Paychecks",
                columns: table => new
                {
                    PaycheckId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryBeforeTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkedHours = table.Column<TimeSpan>(type: "time", nullable: false),
                    AMContribution = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalaryAfterTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VacationPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinanceUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paychecks", x => x.PaycheckId);
                    table.ForeignKey(
                        name: "FK_Paychecks_AspNetUsers_FinanceUserId",
                        column: x => x.FinanceUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paychecks_FinanceUserId",
                table: "Paychecks",
                column: "FinanceUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paychecks");

            migrationBuilder.DropColumn(
                name: "VacationPay",
                table: "Jobs");
        }
    }
}
