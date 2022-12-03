using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class itemDemandLastUpdateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "ItemDemand",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "ItemDemand");
        }
    }
}
