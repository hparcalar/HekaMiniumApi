using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ItemReceiptDetail_Project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ItemReceiptDetail",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_ProjectId",
                table: "ItemReceiptDetail",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceiptDetail_Project_ProjectId",
                table: "ItemReceiptDetail",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceiptDetail_Project_ProjectId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropIndex(
                name: "IX_ItemReceiptDetail_ProjectId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ItemReceiptDetail");
        }
    }
}
