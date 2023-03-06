using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class eInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInvoiced",
                table: "ItemReceiptDetail",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExpenseCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpenseCode = table.Column<int>(type: "integer", nullable: false),
                    ExpenseName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAccountReceipt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceiptType = table.Column<int>(type: "integer", nullable: true),
                    ReceiptNo = table.Column<string>(type: "text", nullable: true),
                    ReceiptDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WorkingPeriodId = table.Column<int>(type: "integer", nullable: true),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentAccountReceipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentAccountReceipt_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CurrentAccountReceipt_WorkingPeriod_WorkingPeriodId",
                        column: x => x.WorkingPeriodId,
                        principalTable: "WorkingPeriod",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FirmPeriodStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmId = table.Column<int>(type: "integer", nullable: false),
                    WorkingPeriodId = table.Column<int>(type: "integer", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    DebitAmount = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmPeriodStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirmPeriodStatus_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FirmPeriodStatus_WorkingPeriod_WorkingPeriodId",
                        column: x => x.WorkingPeriodId,
                        principalTable: "WorkingPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAccountReceiptDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpenseId = table.Column<int>(type: "integer", nullable: true),
                    debit = table.Column<decimal>(type: "numeric", nullable: true),
                    credit = table.Column<decimal>(type: "numeric", nullable: true),
                    CurrentAccountReceiptId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentAccountReceiptDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentAccountReceiptDetail_CurrentAccountReceipt_CurrentAc~",
                        column: x => x.CurrentAccountReceiptId,
                        principalTable: "CurrentAccountReceipt",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CurrentAccountReceiptDetail_ExpenseCard_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "ExpenseCard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceiptNo = table.Column<string>(type: "text", nullable: true),
                    ReceiptType = table.Column<int>(type: "integer", nullable: true),
                    ReceiptDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    IsEInvoice = table.Column<bool>(type: "boolean", nullable: true),
                    EInvoiceStatus = table.Column<bool>(type: "boolean", nullable: true),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    OverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    IsTaxIncluded = table.Column<bool>(type: "boolean", nullable: true),
                    CurrentAccountReceiptId = table.Column<int>(type: "integer", nullable: true),
                    WorkingPeriodId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_CurrentAccountReceipt_CurrentAccountReceiptId",
                        column: x => x.CurrentAccountReceiptId,
                        principalTable: "CurrentAccountReceipt",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoice_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoice_WorkingPeriod_WorkingPeriodId",
                        column: x => x.WorkingPeriodId,
                        principalTable: "WorkingPeriod",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceReceiptDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceId = table.Column<int>(type: "integer", nullable: true),
                    ItemReceiptDetailId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceReceiptDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceReceiptDetail_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceReceiptDetail_ItemReceiptDetail_ItemReceiptDetailId",
                        column: x => x.ItemReceiptDetailId,
                        principalTable: "ItemReceiptDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccountReceipt_FirmId",
                table: "CurrentAccountReceipt",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccountReceipt_WorkingPeriodId",
                table: "CurrentAccountReceipt",
                column: "WorkingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccountReceiptDetail_CurrentAccountReceiptId",
                table: "CurrentAccountReceiptDetail",
                column: "CurrentAccountReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccountReceiptDetail_ExpenseId",
                table: "CurrentAccountReceiptDetail",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_FirmPeriodStatus_FirmId",
                table: "FirmPeriodStatus",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_FirmPeriodStatus_WorkingPeriodId",
                table: "FirmPeriodStatus",
                column: "WorkingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CurrentAccountReceiptId",
                table: "Invoice",
                column: "CurrentAccountReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_FirmId",
                table: "Invoice",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_WorkingPeriodId",
                table: "Invoice",
                column: "WorkingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceReceiptDetail_InvoiceId",
                table: "InvoiceReceiptDetail",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceReceiptDetail_ItemReceiptDetailId",
                table: "InvoiceReceiptDetail",
                column: "ItemReceiptDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentAccountReceiptDetail");

            migrationBuilder.DropTable(
                name: "FirmPeriodStatus");

            migrationBuilder.DropTable(
                name: "InvoiceReceiptDetail");

            migrationBuilder.DropTable(
                name: "ExpenseCard");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "CurrentAccountReceipt");

            migrationBuilder.DropTable(
                name: "WorkingPeriod");

            migrationBuilder.DropColumn(
                name: "IsInvoiced",
                table: "ItemReceiptDetail");
        }
    }
}
