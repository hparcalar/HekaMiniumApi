using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class projectInvoiceTrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryEndDate",
                table: "Project",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpiryExplanation",
                table: "Project",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryStartDate",
                table: "Project",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpiryTime",
                table: "Project",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryEndDate",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ExpiryExplanation",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ExpiryStartDate",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ExpiryTime",
                table: "Project");
        }
    }
}
