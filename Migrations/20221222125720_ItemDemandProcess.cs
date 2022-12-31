using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ItemDemandProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemDemandProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemDemandDetailId = table.Column<int>(type: "integer", nullable: true),
                    ProcessId = table.Column<int>(type: "integer", nullable: true),
                    ProcessOrder = table.Column<int>(type: "integer", nullable: true),
                    ProcessStatus = table.Column<int>(type: "integer", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    AssignedUserId = table.Column<int>(type: "integer", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDemandProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDemandProcess_ItemDemandDetail_ItemDemandDetailId",
                        column: x => x.ItemDemandDetailId,
                        principalTable: "ItemDemandDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemDemandProcess_Process_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Process",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemDemandProcess_SysUser_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "SysUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemandProcess_AssignedUserId",
                table: "ItemDemandProcess",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemandProcess_ItemDemandDetailId",
                table: "ItemDemandProcess",
                column: "ItemDemandDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDemandProcess_ProcessId",
                table: "ItemDemandProcess",
                column: "ProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemDemandProcess");
        }
    }
}
