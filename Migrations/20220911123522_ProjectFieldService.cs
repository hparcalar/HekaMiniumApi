using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ProjectFieldService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectFieldService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: true),
                    ServiceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DocumentNo = table.Column<string>(type: "text", nullable: true),
                    ServiceUserId = table.Column<int>(type: "integer", nullable: true),
                    ServiceStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFieldService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectFieldService_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectFieldService_SysUser_ServiceUserId",
                        column: x => x.ServiceUserId,
                        principalTable: "SysUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectFieldServiceAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectFieldServiceId = table.Column<int>(type: "integer", nullable: true),
                    ProjectFieldServiceDetailId = table.Column<int>(type: "integer", nullable: true),
                    FileContent = table.Column<byte[]>(type: "bytea", nullable: true),
                    FileHeader = table.Column<string>(type: "text", nullable: true),
                    FileExtension = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFieldServiceAttachment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFieldServiceDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectFieldServiceId = table.Column<int>(type: "integer", nullable: true),
                    LineNumber = table.Column<int>(type: "integer", nullable: true),
                    WorkExplanation = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ServiceStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFieldServiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectFieldServiceDetail_ProjectFieldService_ProjectFieldS~",
                        column: x => x.ProjectFieldServiceId,
                        principalTable: "ProjectFieldService",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFieldService_ProjectId",
                table: "ProjectFieldService",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFieldService_ServiceUserId",
                table: "ProjectFieldService",
                column: "ServiceUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFieldServiceDetail_ProjectFieldServiceId",
                table: "ProjectFieldServiceDetail",
                column: "ProjectFieldServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectFieldServiceAttachment");

            migrationBuilder.DropTable(
                name: "ProjectFieldServiceDetail");

            migrationBuilder.DropTable(
                name: "ProjectFieldService");
        }
    }
}
