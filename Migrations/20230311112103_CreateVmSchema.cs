using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace infrastracture_api.Migrations
{
    /// <inheritdoc />
    public partial class CreateVmSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Datacenters_DcId",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Datacenters",
                table: "Datacenters");

            migrationBuilder.DropColumn(
                name: "Fqdn",
                table: "Devices");

            migrationBuilder.RenameTable(
                name: "Devices",
                newName: "devices");

            migrationBuilder.RenameTable(
                name: "Datacenters",
                newName: "datacenters");

            migrationBuilder.RenameColumn(
                name: "OperativeIp",
                table: "devices",
                newName: "IpAddress");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "devices",
                newName: "ServerName");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_DcId",
                table: "devices",
                newName: "IX_devices_DcId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_devices",
                table: "devices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_datacenters",
                table: "datacenters",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "vms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServerName = table.Column<string>(type: "text", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    MgmtIp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vms", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_devices_datacenters_DcId",
                table: "devices",
                column: "DcId",
                principalTable: "datacenters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_devices_datacenters_DcId",
                table: "devices");

            migrationBuilder.DropTable(
                name: "vms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_devices",
                table: "devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_datacenters",
                table: "datacenters");

            migrationBuilder.RenameTable(
                name: "devices",
                newName: "Devices");

            migrationBuilder.RenameTable(
                name: "datacenters",
                newName: "Datacenters");

            migrationBuilder.RenameColumn(
                name: "ServerName",
                table: "Devices",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "Devices",
                newName: "OperativeIp");

            migrationBuilder.RenameIndex(
                name: "IX_devices_DcId",
                table: "Devices",
                newName: "IX_Devices_DcId");

            migrationBuilder.AddColumn<string>(
                name: "Fqdn",
                table: "Devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Datacenters",
                table: "Datacenters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Datacenters_DcId",
                table: "Devices",
                column: "DcId",
                principalTable: "Datacenters",
                principalColumn: "Id");
        }
    }
}
