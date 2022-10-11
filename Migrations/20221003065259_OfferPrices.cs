using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class OfferPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ForexId",
                table: "ItemOfferDetail",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ForexRate",
                table: "ItemOfferDetail",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OverallForexTotal",
                table: "ItemOfferDetail",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OverallTotal",
                table: "ItemOfferDetail",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubForexTotal",
                table: "ItemOfferDetail",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "ItemOfferDetail",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TaxIncluded",
                table: "ItemOfferDetail",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaxRate",
                table: "ItemOfferDetail",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "ItemOfferDetail",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferDetail_ForexId",
                table: "ItemOfferDetail",
                column: "ForexId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOfferDetail_Forex_ForexId",
                table: "ItemOfferDetail",
                column: "ForexId",
                principalTable: "Forex",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemOfferDetail_Forex_ForexId",
                table: "ItemOfferDetail");

            migrationBuilder.DropIndex(
                name: "IX_ItemOfferDetail_ForexId",
                table: "ItemOfferDetail");

            migrationBuilder.DropColumn(
                name: "ForexId",
                table: "ItemOfferDetail");

            migrationBuilder.DropColumn(
                name: "ForexRate",
                table: "ItemOfferDetail");

            migrationBuilder.DropColumn(
                name: "OverallForexTotal",
                table: "ItemOfferDetail");

            migrationBuilder.DropColumn(
                name: "OverallTotal",
                table: "ItemOfferDetail");

            migrationBuilder.DropColumn(
                name: "SubForexTotal",
                table: "ItemOfferDetail");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "ItemOfferDetail");

            migrationBuilder.DropColumn(
                name: "TaxIncluded",
                table: "ItemOfferDetail");

            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "ItemOfferDetail");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "ItemOfferDetail");
        }
    }
}
