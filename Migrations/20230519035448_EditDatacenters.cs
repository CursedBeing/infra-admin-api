using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastracture_api.Migrations
{
    /// <inheritdoc />
    public partial class EditDatacenters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                table: "netdevices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                table: "hypervisors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "datacenters",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "datacenters",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternal",
                table: "datacenters",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "datacenters",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceName",
                table: "netdevices");

            migrationBuilder.DropColumn(
                name: "DeviceName",
                table: "hypervisors");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "datacenters");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "datacenters");

            migrationBuilder.DropColumn(
                name: "IsExternal",
                table: "datacenters");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "datacenters");
        }
    }
}
