using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddtblBaskets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblBaskets",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    UserId = table.Column<short>(nullable: false),
                    Count = table.Column<short>(nullable: false),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBaskets", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_tblBaskets_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBaskets_tblUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "tblUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBaskets_ProductId",
                table: "tblBaskets",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBaskets_UserId1",
                table: "tblBaskets",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBaskets");
        }
    }
}
