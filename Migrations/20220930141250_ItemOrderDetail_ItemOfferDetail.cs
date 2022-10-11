using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ItemOrderDetail_ItemOfferDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemOfferDetailId",
                table: "ItemOrderDetail",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderDetail_ItemOfferDetailId",
                table: "ItemOrderDetail",
                column: "ItemOfferDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrderDetail_ItemOfferDetail_ItemOfferDetailId",
                table: "ItemOrderDetail",
                column: "ItemOfferDetailId",
                principalTable: "ItemOfferDetail",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrderDetail_ItemOfferDetail_ItemOfferDetailId",
                table: "ItemOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_ItemOrderDetail_ItemOfferDetailId",
                table: "ItemOrderDetail");

            migrationBuilder.DropColumn(
                name: "ItemOfferDetailId",
                table: "ItemOrderDetail");
        }
    }
}
