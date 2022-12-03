using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class itemDemandDetail_CreatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ItemDemandDetail",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ItemDemandDetail");
        }
    }
}
