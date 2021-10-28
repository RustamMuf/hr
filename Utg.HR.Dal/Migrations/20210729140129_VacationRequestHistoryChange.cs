using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Utg.HR.Dal.Migrations
{
    public partial class VacationRequestHistoryChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VacationRequestHistoryChanges",
                schema: "public",
                columns: table => new
                {
                    VacationRequestHistoryChangeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    State = table.Column<string>(nullable: false),
                    RequestId = table.Column<int>(nullable: false),
                    VacationRequestId = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationRequestHistoryChanges", x => x.VacationRequestHistoryChangeId);
                    table.ForeignKey(
                        name: "FK_VacationRequestHistoryChanges_VacationRequests_VacationRequ~",
                        column: x => x.VacationRequestId,
                        principalSchema: "public",
                        principalTable: "VacationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequestHistoryChanges_VacationRequestId",
                schema: "public",
                table: "VacationRequestHistoryChanges",
                column: "VacationRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacationRequestHistoryChanges",
                schema: "public");
        }
    }
}
