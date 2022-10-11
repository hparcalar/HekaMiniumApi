using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ItemDemandConsume : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemDemandConsume",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemDemandDetailId = table.Column<int>(type: "integer", nullable: false),
                    ItemOrderDetailId = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    ConsumeDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDemandConsume", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDemandConsume_ItemDemandDetail_ItemDemandDetailId",
                        column: x => x.ItemDemandDetailId,
                        principalTable: "ItemDemandDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemDemandConsume_ItemOrderDetail_ItemOrderDetailId",
                        column: x => x.ItemOrderDetailId,
                        principalTable: "ItemOrderDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemandConsume_ItemDemandDetailId",
                table: "ItemDemandConsume",
                column: "ItemDemandDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemandConsume_ItemOrderDetailId",
                table: "ItemDemandConsume",
                column: "ItemOrderDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemDemandConsume");
        }
    }
}
