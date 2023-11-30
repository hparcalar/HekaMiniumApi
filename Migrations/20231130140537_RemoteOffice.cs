using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class RemoteOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RemoteOffice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Out0 = table.Column<bool>(type: "boolean", nullable: false),
                    Out1 = table.Column<bool>(type: "boolean", nullable: false),
                    Out2 = table.Column<bool>(type: "boolean", nullable: false),
                    Out3 = table.Column<bool>(type: "boolean", nullable: false),
                    Out4 = table.Column<bool>(type: "boolean", nullable: false),
                    Out5 = table.Column<bool>(type: "boolean", nullable: false),
                    Out6 = table.Column<bool>(type: "boolean", nullable: false),
                    Out7 = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteOffice", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemoteOffice");
        }
    }
}
