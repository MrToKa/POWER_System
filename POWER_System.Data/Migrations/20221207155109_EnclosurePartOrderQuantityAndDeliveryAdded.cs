using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POWER_System.Data.Migrations
{
    public partial class EnclosurePartOrderQuantityAndDeliveryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Delivery",
                table: "EnclosurePartOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "EnclosurePartOrders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivery",
                table: "EnclosurePartOrders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "EnclosurePartOrders");
        }
    }
}
