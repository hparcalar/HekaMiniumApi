using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class ProjectManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectCategoryCode = table.Column<string>(type: "text", nullable: true),
                    ProjectCategoryName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PlantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCategory_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTeam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamName = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    LeaderUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTeam_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTeam_SysUser_LeaderUserId",
                        column: x => x.LeaderUserId,
                        principalTable: "SysUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectPhaseTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhaseTemplateCode = table.Column<string>(type: "text", nullable: true),
                    PhaseTemplateName = table.Column<string>(type: "text", nullable: true),
                    ProjectCategoryId = table.Column<int>(type: "integer", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPhaseTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPhaseTemplate_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectPhaseTemplate_ProjectCategory_ProjectCategoryId",
                        column: x => x.ProjectCategoryId,
                        principalTable: "ProjectCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTeamMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserTeamId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeamMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTeamMember_SysUser_UserId",
                        column: x => x.UserId,
                        principalTable: "SysUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTeamMember_UserTeam_UserTeamId",
                        column: x => x.UserTeamId,
                        principalTable: "UserTeam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectCode = table.Column<string>(type: "text", nullable: true),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    ProjectCategoryId = table.Column<int>(type: "integer", nullable: true),
                    ProjectPhaseTemplateId = table.Column<int>(type: "integer", nullable: true),
                    ResponsiblePerson = table.Column<string>(type: "text", nullable: true),
                    ResponsibleInfo = table.Column<string>(type: "text", nullable: true),
                    FirmLocation = table.Column<string>(type: "text", nullable: true),
                    Budget = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Project_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Project_ProjectCategory_ProjectCategoryId",
                        column: x => x.ProjectCategoryId,
                        principalTable: "ProjectCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Project_ProjectPhaseTemplate_ProjectPhaseTemplateId",
                        column: x => x.ProjectPhaseTemplateId,
                        principalTable: "ProjectPhaseTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectPhaseTemplateDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectPhaseTemplateId = table.Column<int>(type: "integer", nullable: true),
                    PhaseTitle = table.Column<string>(type: "text", nullable: true),
                    PhaseColor = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    PhaseOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPhaseTemplateDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPhaseTemplateDetail_ProjectPhaseTemplate_ProjectPhas~",
                        column: x => x.ProjectPhaseTemplateId,
                        principalTable: "ProjectPhaseTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OfferCode = table.Column<string>(type: "text", nullable: true),
                    OfferType = table.Column<int>(type: "integer", nullable: false),
                    OfferDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DocumentNo = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: true),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    OfferStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ForexId = table.Column<int>(type: "integer", nullable: true),
                    SubTotalPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxTotalPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    DiscountTotalPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    OverallTotalPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    CreatedUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offer_Forex_ForexId",
                        column: x => x.ForexId,
                        principalTable: "Forex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offer_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offer_SysUser_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "SysUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectCostItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: true),
                    LineNumber = table.Column<int>(type: "integer", nullable: false),
                    CostType = table.Column<int>(type: "integer", nullable: false),
                    CostStatus = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    CostName = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUserId = table.Column<int>(type: "integer", nullable: true),
                    RealizedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexId = table.Column<int>(type: "integer", nullable: true),
                    EstimatedUnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedForexUnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedForexRate = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedSubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedForexSubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedForexTaxTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedTaxTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedForexOverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedOverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxRate = table.Column<int>(type: "integer", nullable: true),
                    DiscountRate = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexUnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexRate = table.Column<decimal>(type: "numeric", nullable: true),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexSubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexTaxTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexOverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    OverallTotal = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCostItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCostItem_Forex_ForexId",
                        column: x => x.ForexId,
                        principalTable: "Forex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectCostItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectCostItem_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectCostItem_SysUser_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "SysUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectPhase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: true),
                    PhaseTitle = table.Column<string>(type: "text", nullable: true),
                    PhaseColor = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    PhaseOrder = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPhase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPhase_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: true),
                    ProjectPhaseId = table.Column<int>(type: "integer", nullable: true),
                    AssigneeId = table.Column<int>(type: "integer", nullable: true),
                    UserTeamId = table.Column<int>(type: "integer", nullable: true),
                    TaskName = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    TaskStatus = table.Column<int>(type: "integer", nullable: false),
                    EstimatedStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTask_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectTask_ProjectPhase_ProjectPhaseId",
                        column: x => x.ProjectPhaseId,
                        principalTable: "ProjectPhase",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectTask_SysUser_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "SysUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectTask_UserTeam_UserTeamId",
                        column: x => x.UserTeamId,
                        principalTable: "UserTeam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offer_CreatedUserId",
                table: "Offer",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_FirmId",
                table: "Offer",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_ForexId",
                table: "Offer",
                column: "ForexId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_ProjectId",
                table: "Offer",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_FirmId",
                table: "Project",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_PlantId",
                table: "Project",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectCategoryId",
                table: "Project",
                column: "ProjectCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectPhaseTemplateId",
                table: "Project",
                column: "ProjectPhaseTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategory_PlantId",
                table: "ProjectCategory",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCostItem_CreatedUserId",
                table: "ProjectCostItem",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCostItem_ForexId",
                table: "ProjectCostItem",
                column: "ForexId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCostItem_ItemId",
                table: "ProjectCostItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCostItem_ProjectId",
                table: "ProjectCostItem",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhase_ProjectId",
                table: "ProjectPhase",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhaseTemplate_PlantId",
                table: "ProjectPhaseTemplate",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhaseTemplate_ProjectCategoryId",
                table: "ProjectPhaseTemplate",
                column: "ProjectCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhaseTemplateDetail_ProjectPhaseTemplateId",
                table: "ProjectPhaseTemplateDetail",
                column: "ProjectPhaseTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_AssigneeId",
                table: "ProjectTask",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_ProjectId",
                table: "ProjectTask",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_ProjectPhaseId",
                table: "ProjectTask",
                column: "ProjectPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_UserTeamId",
                table: "ProjectTask",
                column: "UserTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeam_LeaderUserId",
                table: "UserTeam",
                column: "LeaderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeam_PlantId",
                table: "UserTeam",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeamMember_UserId",
                table: "UserTeamMember",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeamMember_UserTeamId",
                table: "UserTeamMember",
                column: "UserTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "ProjectCostItem");

            migrationBuilder.DropTable(
                name: "ProjectPhaseTemplateDetail");

            migrationBuilder.DropTable(
                name: "ProjectTask");

            migrationBuilder.DropTable(
                name: "UserTeamMember");

            migrationBuilder.DropTable(
                name: "ProjectPhase");

            migrationBuilder.DropTable(
                name: "UserTeam");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "ProjectPhaseTemplate");

            migrationBuilder.DropTable(
                name: "ProjectCategory");
        }
    }
}
