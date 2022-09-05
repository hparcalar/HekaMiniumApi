using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ItemDemands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemDemandDetailId",
                table: "ItemOrderDetail",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemDemandId",
                table: "ItemOrder",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemDemand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceiptNo = table.Column<string>(type: "text", nullable: true),
                    ReceiptDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeadlineDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: true),
                    IsOrdered = table.Column<bool>(type: "boolean", nullable: true),
                    DemandStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDemand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDemand_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemDemand_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemDemandDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemDemandId = table.Column<int>(type: "integer", nullable: true),
                    LineNumber = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    ItemExplanation = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    UnitId = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    NetQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    DemandStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDemandDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDemandDetail_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemDemandDetail_ItemDemand_ItemDemandId",
                        column: x => x.ItemDemandId,
                        principalTable: "ItemDemand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemDemandDetail_UnitType_UnitId",
                        column: x => x.UnitId,
                        principalTable: "UnitType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderDetail_ItemDemandDetailId",
                table: "ItemOrderDetail",
                column: "ItemDemandDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_ItemDemandId",
                table: "ItemOrder",
                column: "ItemDemandId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemand_PlantId",
                table: "ItemDemand",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemand_ProjectId",
                table: "ItemDemand",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemandDetail_ItemDemandId",
                table: "ItemDemandDetail",
                column: "ItemDemandId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemandDetail_ItemId",
                table: "ItemDemandDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemandDetail_UnitId",
                table: "ItemDemandDetail",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrder_ItemDemand_ItemDemandId",
                table: "ItemOrder",
                column: "ItemDemandId",
                principalTable: "ItemDemand",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrderDetail_ItemDemandDetail_ItemDemandDetailId",
                table: "ItemOrderDetail",
                column: "ItemDemandDetailId",
                principalTable: "ItemDemandDetail",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrder_ItemDemand_ItemDemandId",
                table: "ItemOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrderDetail_ItemDemandDetail_ItemDemandDetailId",
                table: "ItemOrderDetail");

            migrationBuilder.DropTable(
                name: "ItemDemandDetail");

            migrationBuilder.DropTable(
                name: "ItemDemand");

            migrationBuilder.DropIndex(
                name: "IX_ItemOrderDetail_ItemDemandDetailId",
                table: "ItemOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_ItemOrder_ItemDemandId",
                table: "ItemOrder");

            migrationBuilder.DropColumn(
                name: "ItemDemandDetailId",
                table: "ItemOrderDetail");

            migrationBuilder.DropColumn(
                name: "ItemDemandId",
                table: "ItemOrder");
        }
    }
}
