using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class PartNo_And_Dimensions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartDimensions",
                table: "ProjectCostItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartNo",
                table: "ProjectCostItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartDimensions",
                table: "ItemReceiptDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartNo",
                table: "ItemReceiptDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartDimensions",
                table: "ItemOrderDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartNo",
                table: "ItemOrderDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartDimensions",
                table: "ItemDemandDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartNo",
                table: "ItemDemandDetail",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartDimensions",
                table: "ProjectCostItem");

            migrationBuilder.DropColumn(
                name: "PartNo",
                table: "ProjectCostItem");

            migrationBuilder.DropColumn(
                name: "PartDimensions",
                table: "ItemReceiptDetail");

            migrationBuilder.DropColumn(
                name: "PartNo",
                table: "ItemReceiptDetail");

            migrationBuilder.DropColumn(
                name: "PartDimensions",
                table: "ItemOrderDetail");

            migrationBuilder.DropColumn(
                name: "PartNo",
                table: "ItemOrderDetail");

            migrationBuilder.DropColumn(
                name: "PartDimensions",
                table: "ItemDemandDetail");

            migrationBuilder.DropColumn(
                name: "PartNo",
                table: "ItemDemandDetail");
        }
    }
}
