using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class stocktaking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocktaking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StocktakingNo = table.Column<string>(type: "text", nullable: true),
                    StocktakingType = table.Column<int>(type: "integer", nullable: false),
                    StocktakingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    InWarehouseId = table.Column<int>(type: "integer", nullable: true),
                    OutWarehouseId = table.Column<int>(type: "integer", nullable: true),
                    StocktakingStatus = table.Column<int>(type: "integer", nullable: true),
                    SysUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocktaking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocktaking_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stocktaking_SysUser_SysUserId",
                        column: x => x.SysUserId,
                        principalTable: "SysUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stocktaking_Warehouse_InWarehouseId",
                        column: x => x.InWarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stocktaking_Warehouse_OutWarehouseId",
                        column: x => x.OutWarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StocktakingDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StocktakingId = table.Column<int>(type: "integer", nullable: true),
                    LineNumber = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    BrandModelId = table.Column<int>(type: "integer", nullable: true),
                    UnitId = table.Column<int>(type: "integer", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexId = table.Column<int>(type: "integer", nullable: true),
                    ForexRate = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexUnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    AlternatingQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    NetQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    GrossQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    UsedNetQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    DiscountRate = table.Column<decimal>(type: "numeric", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexDiscountPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxIncluded = table.Column<bool>(type: "boolean", nullable: true),
                    TaxRate = table.Column<int>(type: "integer", nullable: true),
                    TaxPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexTaxPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexSubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    OverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexOverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    StocktakingStatus = table.Column<int>(type: "integer", nullable: true),
                    PartNo = table.Column<string>(type: "text", nullable: true),
                    PartDimensions = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StocktakingDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StocktakingDetail_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StocktakingDetail_BrandModel_BrandModelId",
                        column: x => x.BrandModelId,
                        principalTable: "BrandModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StocktakingDetail_Forex_ForexId",
                        column: x => x.ForexId,
                        principalTable: "Forex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StocktakingDetail_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StocktakingDetail_Stocktaking_StocktakingId",
                        column: x => x.StocktakingId,
                        principalTable: "Stocktaking",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StocktakingDetail_UnitType_UnitId",
                        column: x => x.UnitId,
                        principalTable: "UnitType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocktaking_InWarehouseId",
                table: "Stocktaking",
                column: "InWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocktaking_OutWarehouseId",
                table: "Stocktaking",
                column: "OutWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocktaking_PlantId",
                table: "Stocktaking",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocktaking_SysUserId",
                table: "Stocktaking",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StocktakingDetail_BrandId",
                table: "StocktakingDetail",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_StocktakingDetail_BrandModelId",
                table: "StocktakingDetail",
                column: "BrandModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StocktakingDetail_ForexId",
                table: "StocktakingDetail",
                column: "ForexId");

            migrationBuilder.CreateIndex(
                name: "IX_StocktakingDetail_ItemId",
                table: "StocktakingDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StocktakingDetail_StocktakingId",
                table: "StocktakingDetail",
                column: "StocktakingId");

            migrationBuilder.CreateIndex(
                name: "IX_StocktakingDetail_UnitId",
                table: "StocktakingDetail",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StocktakingDetail");

            migrationBuilder.DropTable(
                name: "Stocktaking");
        }
    }
}
