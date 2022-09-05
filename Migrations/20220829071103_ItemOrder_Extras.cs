using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ItemOrder_Extras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ItemOrderDetail",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderDetail_ProjectId",
                table: "ItemOrderDetail",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrderDetail_Project_ProjectId",
                table: "ItemOrderDetail",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrderDetail_Project_ProjectId",
                table: "ItemOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_ItemOrderDetail_ProjectId",
                table: "ItemOrderDetail");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ItemOrderDetail");
        }
    }
}
