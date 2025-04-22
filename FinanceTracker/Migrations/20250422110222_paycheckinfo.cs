using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceTracker.Migrations
{
    /// <inheritdoc />
    public partial class paycheckinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_WorkShifts_Paychecks_PaycheckSalaryPeriod",
                table: "WorkShifts");


            migrationBuilder.AlterColumn<DateOnly>(
                name: "PaycheckSalaryPeriod",
                table: "WorkShifts",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "PaycheckId",
                table: "WorkShifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkShifts_PaycheckId",
                table: "WorkShifts",
                column: "PaycheckId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkShifts_Paychecks_PaycheckSalaryPeriod",
                table: "WorkShifts",
                column: "PaycheckSalaryPeriod",
                principalTable: "Paychecks",
                principalColumn: "SalaryPeriod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkShifts_PaycheckInfos_PaycheckId",
                table: "WorkShifts");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkShifts_Paychecks_PaycheckSalaryPeriod",
                table: "WorkShifts");

            migrationBuilder.DropIndex(
                name: "IX_WorkShifts_PaycheckId",
                table: "WorkShifts");

            migrationBuilder.DropColumn(
                name: "PaycheckId",
                table: "WorkShifts");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PaycheckSalaryPeriod",
                table: "WorkShifts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaycheckInfoId",
                table: "WorkShifts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkShifts_PaycheckInfoId",
                table: "WorkShifts",
                column: "PaycheckInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkShifts_PaycheckInfos_PaycheckInfoId",
                table: "WorkShifts",
                column: "PaycheckInfoId",
                principalTable: "PaycheckInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkShifts_Paychecks_PaycheckSalaryPeriod",
                table: "WorkShifts",
                column: "PaycheckSalaryPeriod",
                principalTable: "Paychecks",
                principalColumn: "SalaryPeriod",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
