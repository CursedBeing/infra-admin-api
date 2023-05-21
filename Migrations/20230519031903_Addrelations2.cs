using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace infrastracture_api.Migrations
{
    /// <inheritdoc />
    public partial class Addrelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hypervisors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OsType = table.Column<string>(type: "text", nullable: true),
                    OsVersion = table.Column<string>(type: "text", nullable: true),
                    MgmtIpAddress = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    DatacenterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hypervisors", x => x.Id);
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PortCount = table.Column<int>(type: "integer", nullable: true),
                    MgmtIpAddress = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    DatacenterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_netdevices", x => x.Id);
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hypervisors");

            migrationBuilder.DropTable(
                name: "netdevices");
        }
    }
}
