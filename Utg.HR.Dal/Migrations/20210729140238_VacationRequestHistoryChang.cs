using Microsoft.EntityFrameworkCore.Migrations;

namespace Utg.HR.Dal.Migrations
{
    public partial class VacationRequestHistoryChang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequestHistoryChanges_VacationRequests_VacationRequ~",
                schema: "public",
                table: "VacationRequestHistoryChanges");

            migrationBuilder.DropIndex(
                name: "IX_VacationRequestHistoryChanges_VacationRequestId",
                schema: "public",
                table: "VacationRequestHistoryChanges");

            migrationBuilder.DropColumn(
                name: "VacationRequestId",
                schema: "public",
                table: "VacationRequestHistoryChanges");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequestHistoryChanges_RequestId",
                schema: "public",
                table: "VacationRequestHistoryChanges",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequestHistoryChanges_VacationRequests_RequestId",
                schema: "public",
                table: "VacationRequestHistoryChanges",
                column: "RequestId",
                principalSchema: "public",
                principalTable: "VacationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequestHistoryChanges_VacationRequests_RequestId",
                schema: "public",
                table: "VacationRequestHistoryChanges");

            migrationBuilder.DropIndex(
                name: "IX_VacationRequestHistoryChanges_RequestId",
                schema: "public",
                table: "VacationRequestHistoryChanges");

            migrationBuilder.AddColumn<int>(
                name: "VacationRequestId",
                schema: "public",
                table: "VacationRequestHistoryChanges",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequestHistoryChanges_VacationRequestId",
                schema: "public",
                table: "VacationRequestHistoryChanges",
                column: "VacationRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequestHistoryChanges_VacationRequests_VacationRequ~",
                schema: "public",
                table: "VacationRequestHistoryChanges",
                column: "VacationRequestId",
                principalSchema: "public",
                principalTable: "VacationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
