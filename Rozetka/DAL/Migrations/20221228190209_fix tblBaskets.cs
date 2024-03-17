using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class fixtblBaskets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBaskets_tblUsers_UserId1",
                table: "tblBaskets");

            migrationBuilder.DropIndex(
                name: "IX_tblBaskets_UserId1",
                table: "tblBaskets");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "tblBaskets");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "tblBaskets",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBaskets_tblUsers_UserId",
                table: "tblBaskets",
                column: "UserId",
                principalTable: "tblUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBaskets_tblUsers_UserId",
                table: "tblBaskets");

            migrationBuilder.AlterColumn<short>(
                name: "UserId",
                table: "tblBaskets",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "tblBaskets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblBaskets_UserId1",
                table: "tblBaskets",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBaskets_tblUsers_UserId1",
                table: "tblBaskets",
                column: "UserId1",
                principalTable: "tblUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
