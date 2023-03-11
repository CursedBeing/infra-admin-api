using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastracture_api.Migrations
{
    /// <inheritdoc />
    public partial class VmsAddForeignKeyWithHost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HostId",
                table: "vms",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_vms_HostId",
                table: "vms",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_vms_devices_HostId",
                table: "vms",
                column: "HostId",
                principalTable: "devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vms_devices_HostId",
                table: "vms");

            migrationBuilder.DropIndex(
                name: "IX_vms_HostId",
                table: "vms");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "vms");
        }
    }
}
