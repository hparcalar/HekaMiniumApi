using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class PartDataForCutting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessType",
                table: "Process",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PartHeight",
                table: "ItemDemandDetail",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PartThickness",
                table: "ItemDemandDetail",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PartWidth",
                table: "ItemDemandDetail",
                type: "numeric",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessType",
                table: "Process");

            migrationBuilder.DropColumn(
                name: "PartHeight",
                table: "ItemDemandDetail");

            migrationBuilder.DropColumn(
                name: "PartThickness",
                table: "ItemDemandDetail");

            migrationBuilder.DropColumn(
                name: "PartWidth",
                table: "ItemDemandDetail");
        }
    }
}
