using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ProjectForexPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ForexRate",
                table: "Project",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferForexPrice",
                table: "Project",
                type: "numeric",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForexRate",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "OfferForexPrice",
                table: "Project");
        }
    }
}
