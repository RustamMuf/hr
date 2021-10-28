using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Utg.HR.Dal.Migrations
{
    public partial class NotificationReadedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReadedDate",
                schema: "public",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadedDate",
                schema: "public",
                table: "Notifications");
        }
    }
}
