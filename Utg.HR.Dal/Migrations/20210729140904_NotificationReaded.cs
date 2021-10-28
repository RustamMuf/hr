using Microsoft.EntityFrameworkCore.Migrations;

namespace Utg.HR.Dal.Migrations
{
    public partial class NotificationReaded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Readed",
                schema: "public",
                table: "Notifications",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Readed",
                schema: "public",
                table: "Notifications");
        }
    }
}
