using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class orderDenial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DenialExplanation",
                table: "ItemOrderDetail",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DenialExplanation",
                table: "ItemOrderDetail");
        }
    }
}
