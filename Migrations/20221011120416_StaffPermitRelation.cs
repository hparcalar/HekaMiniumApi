using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class StaffPermitRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StaffPermit_StaffId",
                table: "StaffPermit",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffPermit_SysUser_StaffId",
                table: "StaffPermit",
                column: "StaffId",
                principalTable: "SysUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffPermit_SysUser_StaffId",
                table: "StaffPermit");

            migrationBuilder.DropIndex(
                name: "IX_StaffPermit_StaffId",
                table: "StaffPermit");
        }
    }
}
