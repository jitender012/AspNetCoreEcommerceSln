using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAddressId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Orders",
                schema: "User",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                 schema: "User",
                table: "Address"
                );

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "User",
                table: "Address");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "User",
                table: "Address",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                schema: "User",
                table: "Address",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
