using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HekaMiniumApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlantCode = table.Column<string>(type: "text", nullable: true),
                    PlantName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    OpenAddress = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Region = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    District = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    DoorNo = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Fax = table.Column<string>(type: "text", nullable: true),
                    Gsm = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressInfo_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandCode = table.Column<string>(type: "text", nullable: true),
                    BrandName = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    RecordIcon = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brand_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FirmCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmCategoryCode = table.Column<string>(type: "text", nullable: true),
                    FirmCategoryName = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirmCategory_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Forex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ForexCode = table.Column<string>(type: "text", nullable: true),
                    ForexName = table.Column<string>(type: "text", nullable: true),
                    LiveRate = table.Column<decimal>(type: "numeric", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forex_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemCategoryCode = table.Column<string>(type: "text", nullable: true),
                    ItemCategoryName = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    RecordIcon = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCategory_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductionLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductionLineCode = table.Column<string>(type: "text", nullable: true),
                    ProductionLineName = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Explanation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionLine_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleCode = table.Column<string>(type: "text", nullable: true),
                    RoleName = table.Column<string>(type: "text", nullable: true),
                    RoleAuthType = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PlantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysRole_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitTypeCode = table.Column<string>(type: "text", nullable: true),
                    UnitTypeName = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitType_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WarehouseCode = table.Column<string>(type: "text", nullable: true),
                    WarehouseName = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    WarehouseType = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    AllowEntry = table.Column<bool>(type: "boolean", nullable: true),
                    AllowDelivery = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouse_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BrandModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    BrandModelCode = table.Column<string>(type: "text", nullable: true),
                    BrandModelName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandModel_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Firm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmCode = table.Column<string>(type: "text", nullable: true),
                    FirmName = table.Column<string>(type: "text", nullable: true),
                    CommercialTitle = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TaxOffice = table.Column<string>(type: "text", nullable: true),
                    TaxNo = table.Column<string>(type: "text", nullable: true),
                    FirmCategoryId = table.Column<int>(type: "integer", nullable: true),
                    FirmType = table.Column<int>(type: "integer", nullable: false),
                    AddressInfoId = table.Column<int>(type: "integer", nullable: true),
                    IsEInvoice = table.Column<bool>(type: "boolean", nullable: true),
                    IsEWaybill = table.Column<bool>(type: "boolean", nullable: true),
                    EInvoiceEndpoint = table.Column<string>(type: "text", nullable: true),
                    EInvoiceLogin = table.Column<string>(type: "text", nullable: true),
                    EInvoicePassword = table.Column<string>(type: "text", nullable: true),
                    EWaybillEndpoint = table.Column<string>(type: "text", nullable: true),
                    EWaybillLogin = table.Column<string>(type: "text", nullable: true),
                    EWaybillPassword = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firm_AddressInfo_AddressInfoId",
                        column: x => x.AddressInfoId,
                        principalTable: "AddressInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Firm_FirmCategory_FirmCategoryId",
                        column: x => x.FirmCategoryId,
                        principalTable: "FirmCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Firm_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Process",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProcessCode = table.Column<string>(type: "text", nullable: true),
                    ProcessName = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EstimatedDuration = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Process_Forex_ForexId",
                        column: x => x.ForexId,
                        principalTable: "Forex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Process_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RouteCode = table.Column<string>(type: "text", nullable: true),
                    RouteName = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    RoutePrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Route_Forex_ForexId",
                        column: x => x.ForexId,
                        principalTable: "Forex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Route_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemGroupCode = table.Column<string>(type: "text", nullable: true),
                    ItemGroupName = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    RecordIcon = table.Column<string>(type: "text", nullable: true),
                    ItemCategoryId = table.Column<int>(type: "integer", nullable: true),
                    OrderInCategory = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemGroup_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Machine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MachineCode = table.Column<string>(type: "text", nullable: true),
                    MachineName = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ProductionLineId = table.Column<int>(type: "integer", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    SerialNo = table.Column<string>(type: "text", nullable: true),
                    InventoryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ProductionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machine_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Machine_ProductionLine_ProductionLineId",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLine",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserCode = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    DefaultLanguage = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    SysRoleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysUser_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SysUser_SysRole_SysRoleId",
                        column: x => x.SysRoleId,
                        principalTable: "SysRole",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FirmAuthor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    AuthorName = table.Column<string>(type: "text", nullable: true),
                    AuthorSurname = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Gsm = table.Column<string>(type: "text", nullable: true),
                    OrderInFirm = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmAuthor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirmAuthor_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceiptNo = table.Column<string>(type: "text", nullable: true),
                    ReceiptType = table.Column<int>(type: "integer", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeadlineDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    DocumentNo = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    IsWaybilled = table.Column<bool>(type: "boolean", nullable: true),
                    ReceiptStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOrder_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOrder_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemReceipt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceiptNo = table.Column<string>(type: "text", nullable: true),
                    ReceiptType = table.Column<int>(type: "integer", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    DocumentNo = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    InWarehouseId = table.Column<int>(type: "integer", nullable: true),
                    OutWarehouseId = table.Column<int>(type: "integer", nullable: true),
                    IsInvoiced = table.Column<bool>(type: "boolean", nullable: true),
                    ReceiptStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemReceipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemReceipt_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceipt_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceipt_Warehouse_InWarehouseId",
                        column: x => x.InWarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceipt_Warehouse_OutWarehouseId",
                        column: x => x.OutWarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemCode = table.Column<string>(type: "text", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: true),
                    ItemType = table.Column<int>(type: "integer", nullable: false),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    RecordIcon = table.Column<string>(type: "text", nullable: true),
                    DefaultTaxRate = table.Column<int>(type: "integer", nullable: true),
                    ItemCategoryId = table.Column<int>(type: "integer", nullable: true),
                    ItemGroupId = table.Column<int>(type: "integer", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    BrandModelId = table.Column<int>(type: "integer", nullable: true),
                    SerialNo = table.Column<string>(type: "text", nullable: true),
                    Barcode = table.Column<string>(type: "text", nullable: true),
                    ProductionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_BrandModel_BrandModelId",
                        column: x => x.BrandModelId,
                        principalTable: "BrandModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemOrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemOrderId = table.Column<int>(type: "integer", nullable: true),
                    LineNumber = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    BrandModelId = table.Column<int>(type: "integer", nullable: true),
                    UnitId = table.Column<int>(type: "integer", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexId = table.Column<int>(type: "integer", nullable: true),
                    ForexRate = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexUnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    AlternatingQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    NetQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    GrossQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    UsedNetQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    DiscountRate = table.Column<decimal>(type: "numeric", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexDiscountPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxIncluded = table.Column<bool>(type: "boolean", nullable: true),
                    TaxRate = table.Column<int>(type: "integer", nullable: true),
                    TaxPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexTaxPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexSubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    OverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexOverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    ReceiptStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOrderDetail_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOrderDetail_BrandModel_BrandModelId",
                        column: x => x.BrandModelId,
                        principalTable: "BrandModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOrderDetail_Forex_ForexId",
                        column: x => x.ForexId,
                        principalTable: "Forex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOrderDetail_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOrderDetail_ItemOrder_ItemOrderId",
                        column: x => x.ItemOrderId,
                        principalTable: "ItemOrder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOrderDetail_UnitType_UnitId",
                        column: x => x.UnitId,
                        principalTable: "UnitType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemReceiptDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemReceiptId = table.Column<int>(type: "integer", nullable: true),
                    LineNumber = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    BrandModelId = table.Column<int>(type: "integer", nullable: true),
                    UnitId = table.Column<int>(type: "integer", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexId = table.Column<int>(type: "integer", nullable: true),
                    ForexRate = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexUnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    AlternatingQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    NetQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    GrossQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    UsedNetQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    DiscountRate = table.Column<decimal>(type: "numeric", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexDiscountPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxIncluded = table.Column<bool>(type: "boolean", nullable: true),
                    TaxRate = table.Column<int>(type: "integer", nullable: true),
                    TaxPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexTaxPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexSubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    OverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    ForexOverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    ReceiptStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemReceiptDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemReceiptDetail_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceiptDetail_BrandModel_BrandModelId",
                        column: x => x.BrandModelId,
                        principalTable: "BrandModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceiptDetail_Forex_ForexId",
                        column: x => x.ForexId,
                        principalTable: "Forex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceiptDetail_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceiptDetail_ItemReceipt_ItemReceiptId",
                        column: x => x.ItemReceiptId,
                        principalTable: "ItemReceipt",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceiptDetail_UnitType_UnitId",
                        column: x => x.UnitId,
                        principalTable: "UnitType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    UnitTypeId = table.Column<int>(type: "integer", nullable: true),
                    IsMainUnit = table.Column<bool>(type: "boolean", nullable: true),
                    IsDefaultUnit = table.Column<bool>(type: "boolean", nullable: true),
                    Divider = table.Column<decimal>(type: "numeric", nullable: true),
                    Multiplier = table.Column<decimal>(type: "numeric", nullable: true),
                    OrderInItem = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemUnit_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemUnit_UnitType_UnitTypeId",
                        column: x => x.UnitTypeId,
                        principalTable: "UnitType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemOrderConsume",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemOrderDetailId = table.Column<int>(type: "integer", nullable: true),
                    ContributerItemReceiptDetailId = table.Column<int>(type: "integer", nullable: true),
                    ConsumerItemReceiptDetailId = table.Column<int>(type: "integer", nullable: true),
                    ContributeNetQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    ConsumeNetQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    ContributeDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ConsumeDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOrderConsume", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOrderConsume_ItemOrderDetail_ItemOrderDetailId",
                        column: x => x.ItemOrderDetailId,
                        principalTable: "ItemOrderDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOrderConsume_ItemReceiptDetail_ConsumerItemReceiptDetai~",
                        column: x => x.ConsumerItemReceiptDetailId,
                        principalTable: "ItemReceiptDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOrderConsume_ItemReceiptDetail_ContributerItemReceiptDe~",
                        column: x => x.ContributerItemReceiptDetailId,
                        principalTable: "ItemReceiptDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemReceiptConsume",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsumedReceiptDetailId = table.Column<int>(type: "integer", nullable: true),
                    ConsumerReceiptDetailId = table.Column<int>(type: "integer", nullable: true),
                    ConsumeDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ConsumeNetQuantity = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemReceiptConsume", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemReceiptConsume_ItemReceiptDetail_ConsumedReceiptDetailId",
                        column: x => x.ConsumedReceiptDetailId,
                        principalTable: "ItemReceiptDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceiptConsume_ItemReceiptDetail_ConsumerReceiptDetailId",
                        column: x => x.ConsumerReceiptDetailId,
                        principalTable: "ItemReceiptDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressInfo_PlantId",
                table: "AddressInfo",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_PlantId",
                table: "Brand",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandModel_BrandId",
                table: "BrandModel",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Firm_AddressInfoId",
                table: "Firm",
                column: "AddressInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Firm_FirmCategoryId",
                table: "Firm",
                column: "FirmCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Firm_PlantId",
                table: "Firm",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_FirmAuthor_FirmId",
                table: "FirmAuthor",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_FirmCategory_PlantId",
                table: "FirmCategory",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Forex_PlantId",
                table: "Forex",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_BrandId",
                table: "Item",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_BrandModelId",
                table: "Item",
                column: "BrandModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemCategoryId",
                table: "Item",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemGroupId",
                table: "Item",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_PlantId",
                table: "Item",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategory_PlantId",
                table: "ItemCategory",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroup_ItemCategoryId",
                table: "ItemGroup",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_FirmId",
                table: "ItemOrder",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_PlantId",
                table: "ItemOrder",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderConsume_ConsumerItemReceiptDetailId",
                table: "ItemOrderConsume",
                column: "ConsumerItemReceiptDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderConsume_ContributerItemReceiptDetailId",
                table: "ItemOrderConsume",
                column: "ContributerItemReceiptDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderConsume_ItemOrderDetailId",
                table: "ItemOrderConsume",
                column: "ItemOrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderDetail_BrandId",
                table: "ItemOrderDetail",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderDetail_BrandModelId",
                table: "ItemOrderDetail",
                column: "BrandModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderDetail_ForexId",
                table: "ItemOrderDetail",
                column: "ForexId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderDetail_ItemId",
                table: "ItemOrderDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderDetail_ItemOrderId",
                table: "ItemOrderDetail",
                column: "ItemOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderDetail_UnitId",
                table: "ItemOrderDetail",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceipt_FirmId",
                table: "ItemReceipt",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceipt_InWarehouseId",
                table: "ItemReceipt",
                column: "InWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceipt_OutWarehouseId",
                table: "ItemReceipt",
                column: "OutWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceipt_PlantId",
                table: "ItemReceipt",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptConsume_ConsumedReceiptDetailId",
                table: "ItemReceiptConsume",
                column: "ConsumedReceiptDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptConsume_ConsumerReceiptDetailId",
                table: "ItemReceiptConsume",
                column: "ConsumerReceiptDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_BrandId",
                table: "ItemReceiptDetail",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_BrandModelId",
                table: "ItemReceiptDetail",
                column: "BrandModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_ForexId",
                table: "ItemReceiptDetail",
                column: "ForexId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_ItemId",
                table: "ItemReceiptDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_ItemReceiptId",
                table: "ItemReceiptDetail",
                column: "ItemReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_UnitId",
                table: "ItemReceiptDetail",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnit_ItemId",
                table: "ItemUnit",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnit_UnitTypeId",
                table: "ItemUnit",
                column: "UnitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Machine_PlantId",
                table: "Machine",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Machine_ProductionLineId",
                table: "Machine",
                column: "ProductionLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Process_ForexId",
                table: "Process",
                column: "ForexId");

            migrationBuilder.CreateIndex(
                name: "IX_Process_PlantId",
                table: "Process",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_PlantId",
                table: "ProductionLine",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_ForexId",
                table: "Route",
                column: "ForexId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_PlantId",
                table: "Route",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRole_PlantId",
                table: "SysRole",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUser_PlantId",
                table: "SysUser",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUser_SysRoleId",
                table: "SysUser",
                column: "SysRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitType_PlantId",
                table: "UnitType",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_PlantId",
                table: "Warehouse",
                column: "PlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirmAuthor");

            migrationBuilder.DropTable(
                name: "ItemOrderConsume");

            migrationBuilder.DropTable(
                name: "ItemReceiptConsume");

            migrationBuilder.DropTable(
                name: "ItemUnit");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "Process");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "SysUser");

            migrationBuilder.DropTable(
                name: "ItemOrderDetail");

            migrationBuilder.DropTable(
                name: "ItemReceiptDetail");

            migrationBuilder.DropTable(
                name: "ProductionLine");

            migrationBuilder.DropTable(
                name: "SysRole");

            migrationBuilder.DropTable(
                name: "ItemOrder");

            migrationBuilder.DropTable(
                name: "Forex");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "ItemReceipt");

            migrationBuilder.DropTable(
                name: "UnitType");

            migrationBuilder.DropTable(
                name: "BrandModel");

            migrationBuilder.DropTable(
                name: "ItemGroup");

            migrationBuilder.DropTable(
                name: "Firm");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "ItemCategory");

            migrationBuilder.DropTable(
                name: "AddressInfo");

            migrationBuilder.DropTable(
                name: "FirmCategory");

            migrationBuilder.DropTable(
                name: "Plant");
        }
    }
}
