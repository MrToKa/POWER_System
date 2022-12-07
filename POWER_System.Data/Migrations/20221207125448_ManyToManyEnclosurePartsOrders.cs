using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POWER_System.Data.Migrations
{
    public partial class ManyToManyEnclosurePartsOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnclosurePart_PartsOrders_PartOrderId",
                table: "EnclosurePart");

            migrationBuilder.DropIndex(
                name: "IX_EnclosurePart_PartOrderId",
                table: "EnclosurePart");

            migrationBuilder.DropColumn(
                name: "PartOrderId",
                table: "EnclosurePart");

            migrationBuilder.CreateTable(
                name: "EnclosurePartOrders",
                columns: table => new
                {
                    PartOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnclosurePartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnclosurePartOrders", x => new { x.EnclosurePartId, x.PartOrderId });
                    table.ForeignKey(
                        name: "FK_EnclosurePartOrders_EnclosurePart_EnclosurePartId",
                        column: x => x.EnclosurePartId,
                        principalTable: "EnclosurePart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnclosurePartOrders_PartsOrders_PartOrderId",
                        column: x => x.PartOrderId,
                        principalTable: "PartsOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnclosurePartOrders_PartOrderId",
                table: "EnclosurePartOrders",
                column: "PartOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnclosurePartOrders");

            migrationBuilder.AddColumn<Guid>(
                name: "PartOrderId",
                table: "EnclosurePart",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnclosurePart_PartOrderId",
                table: "EnclosurePart",
                column: "PartOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnclosurePart_PartsOrders_PartOrderId",
                table: "EnclosurePart",
                column: "PartOrderId",
                principalTable: "PartsOrders",
                principalColumn: "Id");
        }
    }
}
