using Microsoft.EntityFrameworkCore.Migrations;

namespace Utg.HR.Dal.Migrations
{
    public partial class VacationDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Days",
                schema: "public",
                table: "Vacations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Days",
                schema: "public",
                table: "VacationRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                schema: "public",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "Days",
                schema: "public",
                table: "VacationRequests");
        }
    }
}
