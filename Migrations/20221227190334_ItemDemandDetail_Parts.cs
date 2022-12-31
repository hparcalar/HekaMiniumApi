using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ItemDemandDetail_Parts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemDemandDetailPart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemDemandDetailId = table.Column<int>(type: "integer", nullable: true),
                    LineNumber = table.Column<int>(type: "integer", nullable: true),
                    PartNo = table.Column<string>(type: "text", nullable: true),
                    PartHeight = table.Column<decimal>(type: "numeric", nullable: true),
                    PartQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    PartFile = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDemandDetailPart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDemandDetailPart_ItemDemandDetail_ItemDemandDetailId",
                        column: x => x.ItemDemandDetailId,
                        principalTable: "ItemDemandDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemandDetailPart_ItemDemandDetailId",
                table: "ItemDemandDetailPart",
                column: "ItemDemandDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemDemandDetailPart");
        }
    }
}
