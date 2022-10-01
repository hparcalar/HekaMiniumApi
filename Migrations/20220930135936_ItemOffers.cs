using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ItemOffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceiptNo = table.Column<string>(type: "text", nullable: true),
                    ReceiptDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    OfferStatus = table.Column<int>(type: "integer", nullable: true),
                    OfferType = table.Column<int>(type: "integer", nullable: true),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    SysUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOffer_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOffer_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOffer_SysUser_SysUserId",
                        column: x => x.SysUserId,
                        principalTable: "SysUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemOfferDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemOfferId = table.Column<int>(type: "integer", nullable: true),
                    LineNumber = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    UnitId = table.Column<int>(type: "integer", nullable: true),
                    AcceptedFirmId = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    OfferStatus = table.Column<int>(type: "integer", nullable: true),
                    NetQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    ItemExplanation = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    PartNo = table.Column<string>(type: "text", nullable: true),
                    PartDimensions = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOfferDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOfferDetail_Firm_AcceptedFirmId",
                        column: x => x.AcceptedFirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOfferDetail_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOfferDetail_ItemOffer_ItemOfferId",
                        column: x => x.ItemOfferId,
                        principalTable: "ItemOffer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOfferDetail_UnitType_UnitId",
                        column: x => x.UnitId,
                        principalTable: "UnitType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemOfferFirmOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemOfferId = table.Column<int>(type: "integer", nullable: true),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    FirmOrder = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOfferFirmOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOfferFirmOption_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOfferFirmOption_ItemOffer_ItemOfferId",
                        column: x => x.ItemOfferId,
                        principalTable: "ItemOffer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemOfferDetailDemand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemOfferDetailId = table.Column<int>(type: "integer", nullable: true),
                    ItemDemandDetailId = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    DemandOrder = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOfferDetailDemand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOfferDetailDemand_ItemDemandDetail_ItemDemandDetailId",
                        column: x => x.ItemDemandDetailId,
                        principalTable: "ItemDemandDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOfferDetailDemand_ItemOfferDetail_ItemOfferDetailId",
                        column: x => x.ItemOfferDetailId,
                        principalTable: "ItemOfferDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFieldServiceAttachment_ProjectFieldServiceDetailId",
                table: "ProjectFieldServiceAttachment",
                column: "ProjectFieldServiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFieldServiceAttachment_ProjectFieldServiceId",
                table: "ProjectFieldServiceAttachment",
                column: "ProjectFieldServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOffer_FirmId",
                table: "ItemOffer",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOffer_PlantId",
                table: "ItemOffer",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOffer_SysUserId",
                table: "ItemOffer",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferDetail_AcceptedFirmId",
                table: "ItemOfferDetail",
                column: "AcceptedFirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferDetail_ItemId",
                table: "ItemOfferDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferDetail_ItemOfferId",
                table: "ItemOfferDetail",
                column: "ItemOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferDetail_UnitId",
                table: "ItemOfferDetail",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferDetailDemand_ItemDemandDetailId",
                table: "ItemOfferDetailDemand",
                column: "ItemDemandDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferDetailDemand_ItemOfferDetailId",
                table: "ItemOfferDetailDemand",
                column: "ItemOfferDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferFirmOption_FirmId",
                table: "ItemOfferFirmOption",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOfferFirmOption_ItemOfferId",
                table: "ItemOfferFirmOption",
                column: "ItemOfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFieldServiceAttachment_ProjectFieldService_ProjectFi~",
                table: "ProjectFieldServiceAttachment",
                column: "ProjectFieldServiceId",
                principalTable: "ProjectFieldService",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFieldServiceAttachment_ProjectFieldServiceDetail_Pro~",
                table: "ProjectFieldServiceAttachment",
                column: "ProjectFieldServiceDetailId",
                principalTable: "ProjectFieldServiceDetail",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFieldServiceAttachment_ProjectFieldService_ProjectFi~",
                table: "ProjectFieldServiceAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFieldServiceAttachment_ProjectFieldServiceDetail_Pro~",
                table: "ProjectFieldServiceAttachment");

            migrationBuilder.DropTable(
                name: "ItemOfferDetailDemand");

            migrationBuilder.DropTable(
                name: "ItemOfferFirmOption");

            migrationBuilder.DropTable(
                name: "ItemOfferDetail");

            migrationBuilder.DropTable(
                name: "ItemOffer");

            migrationBuilder.DropIndex(
                name: "IX_ProjectFieldServiceAttachment_ProjectFieldServiceDetailId",
                table: "ProjectFieldServiceAttachment");

            migrationBuilder.DropIndex(
                name: "IX_ProjectFieldServiceAttachment_ProjectFieldServiceId",
                table: "ProjectFieldServiceAttachment");
        }
    }
}
