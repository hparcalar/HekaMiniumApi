using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class Project_Quantity_And_Forex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ForexId",
                table: "Project",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Project",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_ForexId",
                table: "Project",
                column: "ForexId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Forex_ForexId",
                table: "Project",
                column: "ForexId",
                principalTable: "Forex",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Forex_ForexId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ForexId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ForexId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Project");
        }
    }
}
