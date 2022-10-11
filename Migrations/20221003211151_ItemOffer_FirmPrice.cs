using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ItemOffer_FirmPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemOfferFirmPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemOfferDetailId = table.Column<int>(type: "integer", nullable: true),
                    ItemOfferId = table.Column<int>(type: "integer", nullable: true),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    ForexId = table.Column<int>(type: "integer", nullable: true),
                    ForexRate = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    SubForexTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxIncluded = table.Column<bool>(type: "boolean", nullable: true),
                    TaxRate = table.Column<int>(type: "integer", nullable: true),
                    OverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    OverallForexTotal = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOfferFirmPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOfferFirmPrice_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOfferFirmPrice_Forex_ForexId",
                        column: x => x.ForexId,
                        principalTable: "Forex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOfferFirmPrice_ItemOffer_ItemOfferId",
                        column: x => x.ItemOfferId,
                        principalTable: "ItemOffer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOfferFirmPrice_ItemOfferDetail_ItemOfferDetailId",
                        column: x => x.ItemOfferDetailId,
                        principalTable: "ItemOfferDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferFirmPrice_FirmId",
                table: "ItemOfferFirmPrice",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferFirmPrice_ForexId",
                table: "ItemOfferFirmPrice",
                column: "ForexId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferFirmPrice_ItemOfferDetailId",
                table: "ItemOfferFirmPrice",
                column: "ItemOfferDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferFirmPrice_ItemOfferId",
                table: "ItemOfferFirmPrice",
                column: "ItemOfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemOfferFirmPrice");
        }
    }
}
