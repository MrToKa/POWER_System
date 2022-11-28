using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POWER_System.Models.Migrations
{
    public partial class PartListsRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnclosurePart");

            migrationBuilder.AddColumn<Guid>(
                name: "EnclosureId",
                table: "Parts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_EnclosureId",
                table: "Parts",
                column: "EnclosureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Enclosure_EnclosureId",
                table: "Parts",
                column: "EnclosureId",
                principalTable: "Enclosure",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Enclosure_EnclosureId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_EnclosureId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "EnclosureId",
                table: "Parts");

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

            migrationBuilder.CreateIndex(
                name: "IX_EnclosurePart_PartsId",
                table: "EnclosurePart",
                column: "PartsId");
        }
    }
}
