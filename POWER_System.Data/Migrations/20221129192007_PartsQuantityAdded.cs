using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POWER_System.Data.Migrations
{
    public partial class PartsQuantityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartTag_Capacity",
                table: "EnclosurePart",
                newName: "Id");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Parts",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "EnclosurePart",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "PartTagsQuantities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnclosurePartPartId = table.Column<int>(type: "int", nullable: false),
                    EnclosurePartEnclosureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartTagsQuantities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartTagsQuantities_EnclosurePart_EnclosurePartPartId_EnclosurePartEnclosureId",
                        columns: x => new { x.EnclosurePartPartId, x.EnclosurePartEnclosureId },
                        principalTable: "EnclosurePart",
                        principalColumns: new[] { "PartId", "EnclosureId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartTagsQuantities_EnclosurePartPartId_EnclosurePartEnclosureId",
                table: "PartTagsQuantities",
                columns: new[] { "EnclosurePartPartId", "EnclosurePartEnclosureId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartTagsQuantities");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EnclosurePart",
                newName: "PartTag_Capacity");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Parts",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "EnclosurePart",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
