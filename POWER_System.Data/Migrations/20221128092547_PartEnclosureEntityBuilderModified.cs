using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POWER_System.Models.Migrations
{
    public partial class PartEnclosureEntityBuilderModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Enclosure_EnclosureId",
                table: "Parts");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Enclosure_EnclosureId",
                table: "Parts",
                column: "EnclosureId",
                principalTable: "Enclosure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Enclosure_EnclosureId",
                table: "Parts");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Enclosure_EnclosureId",
                table: "Parts",
                column: "EnclosureId",
                principalTable: "Enclosure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
