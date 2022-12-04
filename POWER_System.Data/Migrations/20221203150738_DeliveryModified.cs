using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POWER_System.Data.Migrations
{
    public partial class DeliveryModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Delivery",
                table: "EnclosurePart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivery",
                table: "EnclosurePart");
        }
    }
}
