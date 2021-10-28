using Microsoft.EntityFrameworkCore.Migrations;

namespace Utg.HR.Dal.Migrations
{
    public partial class VacationIsPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPayment",
                schema: "public",
                table: "Vacations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPayment",
                schema: "public",
                table: "VacationRequests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPayment",
                schema: "public",
                table: "VacationOrders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPayment",
                schema: "public",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "IsPayment",
                schema: "public",
                table: "VacationRequests");

            migrationBuilder.DropColumn(
                name: "IsPayment",
                schema: "public",
                table: "VacationOrders");
        }
    }
}
