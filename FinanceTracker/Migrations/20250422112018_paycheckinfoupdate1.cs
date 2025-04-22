using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceTracker.Migrations
{
    /// <inheritdoc />
    public partial class paycheckinfoupdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkShifts_AspNetUsers_FinanceUserId1",
                table: "WorkShifts");

            migrationBuilder.RenameColumn(
                name: "FinanceUserId1",
                table: "WorkShifts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkShifts_FinanceUserId1",
                table: "WorkShifts",
                newName: "IX_WorkShifts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkShifts_AspNetUsers_UserId",
                table: "WorkShifts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkShifts_AspNetUsers_UserId",
                table: "WorkShifts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WorkShifts",
                newName: "FinanceUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_WorkShifts_UserId",
                table: "WorkShifts",
                newName: "IX_WorkShifts_FinanceUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkShifts_AspNetUsers_FinanceUserId1",
                table: "WorkShifts",
                column: "FinanceUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
