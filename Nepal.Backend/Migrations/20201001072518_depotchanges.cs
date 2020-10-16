using Microsoft.EntityFrameworkCore.Migrations;

namespace Nepal.Backend.Migrations
{
    public partial class depotchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Depots",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Depots",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalesPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepotId = table.Column<int>(nullable: false),
                    Productid = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesPrices_Depots_DepotId",
                        column: x => x.DepotId,
                        principalTable: "Depots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesPrices_Products_Productid",
                        column: x => x.Productid,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesPrices_DepotId",
                table: "SalesPrices",
                column: "DepotId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesPrices_Productid",
                table: "SalesPrices",
                column: "Productid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesPrices");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Depots");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "Depots");

            migrationBuilder.DropColumn(
                name: "UserNo",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
