using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastracture_api.Migrations
{
    /// <inheritdoc />
    public partial class EditHostType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hosts_HostTypes_TypeId",
                table: "Hosts");

            migrationBuilder.AlterColumn<long>(
                name: "TypeId",
                table: "Hosts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Hosts_HostTypes_TypeId",
                table: "Hosts",
                column: "TypeId",
                principalTable: "HostTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hosts_HostTypes_TypeId",
                table: "Hosts");

            migrationBuilder.AlterColumn<long>(
                name: "TypeId",
                table: "Hosts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hosts_HostTypes_TypeId",
                table: "Hosts",
                column: "TypeId",
                principalTable: "HostTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
