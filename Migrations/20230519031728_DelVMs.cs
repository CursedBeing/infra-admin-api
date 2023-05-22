using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace infrastracture_api.Migrations
{
    /// <inheritdoc />
    public partial class DelVMs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vms");

            migrationBuilder.DropTable(
                name: "Device");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DatacenterId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Device_datacenters_DatacenterId",
                        column: x => x.DatacenterId,
                        principalTable: "datacenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "vms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HostId = table.Column<long>(type: "bigint", nullable: true),
                    AddedToMonintoring = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    MgmtIp = table.Column<string>(type: "text", nullable: true),
                    ServerName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vms_Device_HostId",
                        column: x => x.HostId,
                        principalTable: "Device",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Device_DatacenterId",
                table: "Device",
                column: "DatacenterId");

            migrationBuilder.CreateIndex(
                name: "IX_vms_HostId",
                table: "vms",
                column: "HostId");
        }
    }
}
