using Microsoft.EntityFrameworkCore.Migrations;

namespace Nepal.Backend.Migrations
{
    public partial class ordercredit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Credits_CreditId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CreditId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreditId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrderCredits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    CreditId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCredits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderCredits_Credits_CreditId",
                        column: x => x.CreditId,
                        principalTable: "Credits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderCredits_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderCredits_CreditId",
                table: "OrderCredits",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCredits_OrderId",
                table: "OrderCredits",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderCredits");

            migrationBuilder.AddColumn<int>(
                name: "CreditId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreditId",
                table: "Orders",
                column: "CreditId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Credits_CreditId",
                table: "Orders",
                column: "CreditId",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
