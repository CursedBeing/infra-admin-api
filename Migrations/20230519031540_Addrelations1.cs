using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastracture_api.Migrations
{
    /// <inheritdoc />
    public partial class Addrelations1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hypervisors");

            migrationBuilder.DropTable(
                name: "netdevices");

            migrationBuilder.AddColumn<long>(
                name: "DatacenterId",
                table: "Device",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_DatacenterId",
                table: "Device",
                column: "DatacenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_datacenters_DatacenterId",
                table: "Device",
                column: "DatacenterId",
                principalTable: "datacenters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_datacenters_DatacenterId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Device_DatacenterId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "DatacenterId",
                table: "Device");

            migrationBuilder.CreateTable(
                name: "hypervisors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DatacenterId = table.Column<long>(type: "bigint", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    MgmtIpAddress = table.Column<string>(type: "text", nullable: true),
                    OsType = table.Column<string>(type: "text", nullable: true),
                    OsVersion = table.Column<string>(type: "text", nullable: true)
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
                    DatacenterId = table.Column<long>(type: "bigint", nullable: true),
                    MgmtIpAddress = table.Column<string>(type: "text", nullable: true),
                    PortCount = table.Column<int>(type: "integer", nullable: true)
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
        }
    }
}
