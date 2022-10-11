using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class CreatedUsersOf_Demand_And_Receipts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SysUserId",
                table: "ItemReceipt",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SysUserId",
                table: "ItemOrder",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SysUserId",
                table: "ItemDemand",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceipt_SysUserId",
                table: "ItemReceipt",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_SysUserId",
                table: "ItemOrder",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemand_SysUserId",
                table: "ItemDemand",
                column: "SysUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDemand_SysUser_SysUserId",
                table: "ItemDemand",
                column: "SysUserId",
                principalTable: "SysUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrder_SysUser_SysUserId",
                table: "ItemOrder",
                column: "SysUserId",
                principalTable: "SysUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceipt_SysUser_SysUserId",
                table: "ItemReceipt",
                column: "SysUserId",
                principalTable: "SysUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemDemand_SysUser_SysUserId",
                table: "ItemDemand");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrder_SysUser_SysUserId",
                table: "ItemOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceipt_SysUser_SysUserId",
                table: "ItemReceipt");

            migrationBuilder.DropIndex(
                name: "IX_ItemReceipt_SysUserId",
                table: "ItemReceipt");

            migrationBuilder.DropIndex(
                name: "IX_ItemOrder_SysUserId",
                table: "ItemOrder");

            migrationBuilder.DropIndex(
                name: "IX_ItemDemand_SysUserId",
                table: "ItemDemand");

            migrationBuilder.DropColumn(
                name: "SysUserId",
                table: "ItemReceipt");

            migrationBuilder.DropColumn(
                name: "SysUserId",
                table: "ItemOrder");

            migrationBuilder.DropColumn(
                name: "SysUserId",
                table: "ItemDemand");
        }
    }
}
