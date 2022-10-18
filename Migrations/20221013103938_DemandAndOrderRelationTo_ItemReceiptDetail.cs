using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class DemandAndOrderRelationTo_ItemReceiptDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemDemandDetailId",
                table: "ItemReceiptDetail",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemOrderDetailId",
                table: "ItemReceiptDetail",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_ItemDemandDetailId",
                table: "ItemReceiptDetail",
                column: "ItemDemandDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_ItemOrderDetailId",
                table: "ItemReceiptDetail",
                column: "ItemOrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceiptDetail_ItemDemandDetail_ItemDemandDetailId",
                table: "ItemReceiptDetail",
                column: "ItemDemandDetailId",
                principalTable: "ItemDemandDetail",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceiptDetail_ItemOrderDetail_ItemOrderDetailId",
                table: "ItemReceiptDetail",
                column: "ItemOrderDetailId",
                principalTable: "ItemOrderDetail",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceiptDetail_ItemDemandDetail_ItemDemandDetailId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceiptDetail_ItemOrderDetail_ItemOrderDetailId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropIndex(
                name: "IX_ItemReceiptDetail_ItemDemandDetailId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropIndex(
                name: "IX_ItemReceiptDetail_ItemOrderDetailId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropColumn(
                name: "ItemDemandDetailId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropColumn(
                name: "ItemOrderDetailId",
                table: "ItemReceiptDetail");
        }
    }
}
