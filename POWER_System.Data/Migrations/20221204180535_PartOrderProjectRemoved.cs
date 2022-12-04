using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POWER_System.Data.Migrations
{
    public partial class PartOrderProjectRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartsOrders_Enclosure_EnclosureId",
                table: "PartsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PartsOrders_Projects_ProjectId",
                table: "PartsOrders");

            migrationBuilder.DropColumn(
                name: "EnclosureTag",
                table: "PartsOrders");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "PartsOrders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnclosureId",
                table: "PartsOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PartsOrders_Enclosure_EnclosureId",
                table: "PartsOrders",
                column: "EnclosureId",
                principalTable: "Enclosure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartsOrders_Projects_ProjectId",
                table: "PartsOrders",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartsOrders_Enclosure_EnclosureId",
                table: "PartsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PartsOrders_Projects_ProjectId",
                table: "PartsOrders");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "PartsOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EnclosureId",
                table: "PartsOrders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "EnclosureTag",
                table: "PartsOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PartsOrders_Enclosure_EnclosureId",
                table: "PartsOrders",
                column: "EnclosureId",
                principalTable: "Enclosure",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PartsOrders_Projects_ProjectId",
                table: "PartsOrders",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
