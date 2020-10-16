using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nepal.Backend.Migrations
{
    public partial class creditdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreditDate",
                table: "Credits",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditDate",
                table: "Credits");
        }
    }
}
