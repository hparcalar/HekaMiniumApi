using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class AttachmentExtensions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttachmentCategoryId",
                table: "Attachment",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartNo",
                table: "Attachment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubParts",
                table: "Attachment",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AttachmentCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttachmentCategory_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_AttachmentCategoryId",
                table: "Attachment",
                column: "AttachmentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentCategory_PlantId",
                table: "AttachmentCategory",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_AttachmentCategory_AttachmentCategoryId",
                table: "Attachment",
                column: "AttachmentCategoryId",
                principalTable: "AttachmentCategory",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_AttachmentCategory_AttachmentCategoryId",
                table: "Attachment");

            migrationBuilder.DropTable(
                name: "AttachmentCategory");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_AttachmentCategoryId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "AttachmentCategoryId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "PartNo",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "SubParts",
                table: "Attachment");
        }
    }
}
