using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POWER_System.Models.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Contractor = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RequestedFrom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequiredTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enclosure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Plant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Revision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enclosure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enclosure_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectSiteService",
                columns: table => new
                {
                    ProjectsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSiteService", x => new { x.ProjectsId, x.SiteServicesId });
                    table.ForeignKey(
                        name: "FK_ProjectSiteService_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSiteService_SiteServices_SiteServicesId",
                        column: x => x.SiteServicesId,
                        principalTable: "SiteServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CablesOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnclosureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CablesOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CablesOrders_Enclosure_EnclosureId",
                        column: x => x.EnclosureId,
                        principalTable: "Enclosure",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CablesOrders_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CablesOrders_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PartsOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartsOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartsOrders_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartsOrders_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Conductors = table.Column<int>(type: "int", nullable: false),
                    CrossSection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromLocationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToLocationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voltage = table.Column<int>(type: "int", nullable: false),
                    Routing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignLength = table.Column<int>(type: "int", nullable: false),
                    InstallLength = table.Column<int>(type: "int", nullable: true),
                    PullDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConnectedFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConnectedTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnclosureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastChangeDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CableOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SiteServiceId = table.Column<int>(type: "int", nullable: true),
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cables_CablesOrders_CableOrderId",
                        column: x => x.CableOrderId,
                        principalTable: "CablesOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cables_Enclosure_EnclosureId",
                        column: x => x.EnclosureId,
                        principalTable: "Enclosure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cables_SiteServices_SiteServiceId",
                        column: x => x.SiteServiceId,
                        principalTable: "SiteServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cables_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manufacturer = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Measure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Delivery = table.Column<int>(type: "int", nullable: false),
                    PartOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SiteServiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parts_PartsOrders_PartOrderId",
                        column: x => x.PartOrderId,
                        principalTable: "PartsOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parts_SiteServices_SiteServiceId",
                        column: x => x.SiteServiceId,
                        principalTable: "SiteServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EnclosurePart",
                columns: table => new
                {
                    EnclosureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnclosurePart", x => new { x.EnclosureId, x.PartsId });
                    table.ForeignKey(
                        name: "FK_EnclosurePart_Enclosure_EnclosureId",
                        column: x => x.EnclosureId,
                        principalTable: "Enclosure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnclosurePart_Parts_PartsId",
                        column: x => x.PartsId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartStorage",
                columns: table => new
                {
                    PartsId = table.Column<int>(type: "int", nullable: false),
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartStorage", x => new { x.PartsId, x.StorageId });
                    table.ForeignKey(
                        name: "FK_PartStorage_Parts_PartsId",
                        column: x => x.PartsId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartStorage_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cables_CableOrderId",
                table: "Cables",
                column: "CableOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Cables_EnclosureId",
                table: "Cables",
                column: "EnclosureId");

            migrationBuilder.CreateIndex(
                name: "IX_Cables_SiteServiceId",
                table: "Cables",
                column: "SiteServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cables_StorageId",
                table: "Cables",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_CablesOrders_EnclosureId",
                table: "CablesOrders",
                column: "EnclosureId");

            migrationBuilder.CreateIndex(
                name: "IX_CablesOrders_ProjectId",
                table: "CablesOrders",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CablesOrders_StorageId",
                table: "CablesOrders",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Enclosure_ProjectId",
                table: "Enclosure",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EnclosurePart_PartsId",
                table: "EnclosurePart",
                column: "PartsId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_StorageId",
                table: "Equipment",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PartOrderId",
                table: "Parts",
                column: "PartOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_SiteServiceId",
                table: "Parts",
                column: "SiteServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsOrders_ProjectId",
                table: "PartsOrders",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsOrders_StorageId",
                table: "PartsOrders",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_PartStorage_StorageId",
                table: "PartStorage",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSiteService_SiteServicesId",
                table: "ProjectSiteService",
                column: "SiteServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_ProjectId",
                table: "Storages",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cables");

            migrationBuilder.DropTable(
                name: "EnclosurePart");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "PartStorage");

            migrationBuilder.DropTable(
                name: "ProjectSiteService");

            migrationBuilder.DropTable(
                name: "CablesOrders");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Enclosure");

            migrationBuilder.DropTable(
                name: "PartsOrders");

            migrationBuilder.DropTable(
                name: "SiteServices");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
