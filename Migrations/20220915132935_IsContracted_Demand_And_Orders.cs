using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class IsContracted_Demand_And_Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsContracted",
                table: "ItemOrderDetail",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsContracted",
                table: "ItemOrder",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsContracted",
                table: "ItemDemandDetail",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsContracted",
                table: "ItemDemand",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsContracted",
                table: "ItemOrderDetail");

            migrationBuilder.DropColumn(
                name: "IsContracted",
                table: "ItemOrder");

            migrationBuilder.DropColumn(
                name: "IsContracted",
                table: "ItemDemandDetail");

            migrationBuilder.DropColumn(
                name: "IsContracted",
                table: "ItemDemand");
        }
    }
}
