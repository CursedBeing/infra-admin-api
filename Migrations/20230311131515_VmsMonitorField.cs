using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastracture_api.Migrations
{
    /// <inheritdoc />
    public partial class VmsMonitorField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AddedToMonintoring",
                table: "vms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "vms",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedToMonintoring",
                table: "vms");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "vms");
        }
    }
}
