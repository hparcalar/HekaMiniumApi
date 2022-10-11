using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class Project_ProfitAndOfferPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OfferPrice",
                table: "Project",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfitRate",
                table: "Project",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferPrice",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProfitRate",
                table: "Project");
        }
    }
}
