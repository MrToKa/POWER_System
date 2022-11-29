using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POWER_System.Data.Migrations
{
    public partial class EnclosurePartChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartTag",
                table: "EnclosurePart");

            migrationBuilder.AddColumn<int>(
                name: "PartTag_Capacity",
                table: "EnclosurePart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartTag_Capacity",
                table: "EnclosurePart");

            migrationBuilder.AddColumn<string>(
                name: "PartTag",
                table: "EnclosurePart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
