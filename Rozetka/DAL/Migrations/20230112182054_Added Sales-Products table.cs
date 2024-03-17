using Microsoft.EntityFrameworkCore.Migrations;


namespace DAL.Migrations
{
    public partial class AddedSalesProductstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblSales_ProductEnity",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    SaleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSales_ProductEnity", x => new { x.SaleId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_tblSales_ProductEnity_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSales_ProductEnity_tblSales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "tblSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblSales_ProductEnity_ProductId",
                table: "tblSales_ProductEnity",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblSales_ProductEnity");
        }
    }
}
