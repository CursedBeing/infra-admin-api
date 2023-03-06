using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastracture_api.Migrations
{
    /// <inheritdoc />
    public partial class AddHostTypeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Targets_HostType_TypeId",
                table: "Targets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Targets",
                table: "Targets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HostType",
                table: "HostType");

            migrationBuilder.RenameTable(
                name: "Targets",
                newName: "Hosts");

            migrationBuilder.RenameTable(
                name: "HostType",
                newName: "HostTypes");

            migrationBuilder.RenameIndex(
                name: "IX_Targets_TypeId",
                table: "Hosts",
                newName: "IX_Hosts_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hosts",
                table: "Hosts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HostTypes",
                table: "HostTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hosts_HostTypes_TypeId",
                table: "Hosts",
                column: "TypeId",
                principalTable: "HostTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hosts_HostTypes_TypeId",
                table: "Hosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HostTypes",
                table: "HostTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hosts",
                table: "Hosts");

            migrationBuilder.RenameTable(
                name: "HostTypes",
                newName: "HostType");

            migrationBuilder.RenameTable(
                name: "Hosts",
                newName: "Targets");

            migrationBuilder.RenameIndex(
                name: "IX_Hosts_TypeId",
                table: "Targets",
                newName: "IX_Targets_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HostType",
                table: "HostType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Targets",
                table: "Targets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_HostType_TypeId",
                table: "Targets",
                column: "TypeId",
                principalTable: "HostType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
