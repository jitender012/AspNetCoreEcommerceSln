using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressRefInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {          

            migrationBuilder.DropIndex(
                name: "IX_Orders_BillingAddressId",
                schema: "User",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ShippingAddressId",
                schema: "User",
                table: "Orders",
                newName: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                schema: "User",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Address_AddressId",
                schema: "User",
                table: "Orders",
                column: "AddressId",
                principalSchema: "User",
                principalTable: "Address",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Address_AddressId",
                schema: "User",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AddressId",
                schema: "User",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                schema: "User",
                table: "Orders",
                newName: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillingAddressId",
                schema: "User",
                table: "Orders",
                column: "BillingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Orders",
                schema: "User",
                table: "Orders",
                column: "BillingAddressId",
                principalSchema: "User",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
