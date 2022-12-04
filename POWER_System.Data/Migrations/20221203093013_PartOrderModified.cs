using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POWER_System.Data.Migrations
{
    public partial class PartOrderModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_PartsOrders_PartOrderId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_PartOrderId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "PartOrderId",
                table: "Parts");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "PartsOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "EnclosureId",
                table: "PartsOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnclosureTag",
                table: "PartsOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PartOrderId",
                table: "EnclosurePart",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Enclosure",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Enclosure",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Enclosure",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "CablesOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PartsOrders_EnclosureId",
                table: "PartsOrders",
                column: "EnclosureId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PartsOrders_Enclosure_EnclosureId",
                table: "PartsOrders",
                column: "EnclosureId",
                principalTable: "Enclosure",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnclosurePart_PartsOrders_PartOrderId",
                table: "EnclosurePart");

            migrationBuilder.DropForeignKey(
                name: "FK_PartsOrders_Enclosure_EnclosureId",
                table: "PartsOrders");

            migrationBuilder.DropIndex(
                name: "IX_PartsOrders_EnclosureId",
                table: "PartsOrders");

            migrationBuilder.DropIndex(
                name: "IX_EnclosurePart_PartOrderId",
                table: "EnclosurePart");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "PartsOrders");

            migrationBuilder.DropColumn(
                name: "EnclosureId",
                table: "PartsOrders");

            migrationBuilder.DropColumn(
                name: "EnclosureTag",
                table: "PartsOrders");

            migrationBuilder.DropColumn(
                name: "PartOrderId",
                table: "EnclosurePart");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Enclosure");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Enclosure");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Enclosure");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "CablesOrders");

            migrationBuilder.AddColumn<Guid>(
                name: "PartOrderId",
                table: "Parts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PartOrderId",
                table: "Parts",
                column: "PartOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_PartsOrders_PartOrderId",
                table: "Parts",
                column: "PartOrderId",
                principalTable: "PartsOrders",
                principalColumn: "Id");
        }
    }
}
