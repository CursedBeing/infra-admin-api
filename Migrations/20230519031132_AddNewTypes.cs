using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastracture_api.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_devices_datacenters_DcId",
                table: "devices");

            migrationBuilder.DropForeignKey(
                name: "FK_vms_devices_HostId",
                table: "vms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_devices",
                table: "devices");

            migrationBuilder.DropIndex(
                name: "IX_devices_DcId",
                table: "devices");

            migrationBuilder.DropColumn(
                name: "DcId",
                table: "devices");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "devices");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "devices");

            migrationBuilder.DropColumn(
                name: "MgmtIp",
                table: "devices");

            migrationBuilder.DropColumn(
                name: "ServerName",
                table: "devices");

            migrationBuilder.RenameTable(
                name: "devices",
                newName: "Device");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Device",
                table: "Device",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "hypervisors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OsType = table.Column<string>(type: "text", nullable: true),
                    OsVersion = table.Column<string>(type: "text", nullable: true),
                    MgmtIpAddress = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    DatacenterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hypervisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hypervisors_Device_Id",
                        column: x => x.Id,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hypervisors_datacenters_DatacenterId",
                        column: x => x.DatacenterId,
                        principalTable: "datacenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "netdevices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PortCount = table.Column<int>(type: "integer", nullable: true),
                    MgmtIpAddress = table.Column<string>(type: "text", nullable: true),
                    DatacenterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_netdevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_netdevices_Device_Id",
                        column: x => x.Id,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_netdevices_datacenters_DatacenterId",
                        column: x => x.DatacenterId,
                        principalTable: "datacenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_hypervisors_DatacenterId",
                table: "hypervisors",
                column: "DatacenterId");

            migrationBuilder.CreateIndex(
                name: "IX_netdevices_DatacenterId",
                table: "netdevices",
                column: "DatacenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_vms_Device_HostId",
                table: "vms",
                column: "HostId",
                principalTable: "Device",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vms_Device_HostId",
                table: "vms");

            migrationBuilder.DropTable(
                name: "hypervisors");

            migrationBuilder.DropTable(
                name: "netdevices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Device",
                table: "Device");

            migrationBuilder.RenameTable(
                name: "Device",
                newName: "devices");

            migrationBuilder.AddColumn<long>(
                name: "DcId",
                table: "devices",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MgmtIp",
                table: "devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerName",
                table: "devices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_devices",
                table: "devices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_devices_DcId",
                table: "devices",
                column: "DcId");

            migrationBuilder.AddForeignKey(
                name: "FK_devices_datacenters_DcId",
                table: "devices",
                column: "DcId",
                principalTable: "datacenters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_vms_devices_HostId",
                table: "vms",
                column: "HostId",
                principalTable: "devices",
                principalColumn: "Id");
        }
    }
}
