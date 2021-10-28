using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Utg.HR.Dal.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.EnsureSchema(
				name: "public");

			migrationBuilder.CreateTable(
				name: "HrRequests",
				schema: "public",
				columns: table => new
				{
					HrRequestId = table.Column<int>(nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					Name = table.Column<string>(nullable: true),
					CreatedDate = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_HrRequests", x => x.HrRequestId);
				});
			
			migrationBuilder.Sql(@"insert into public.""HrRequests""  (""Name"", ""CreatedDate"") values ('Заявление на увольнение', current_date);");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "HrRequests",
				schema: "public");
		}
	}
}
