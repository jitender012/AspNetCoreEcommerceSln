using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class syncDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ✅ Rename Price to UnitPrice
            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "User",
                table: "CartItem",
                newName: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                schema: "Inventory",
                table: "Warehouse");

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                schema: "Inventory",
                table: "Warehouse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Warehouse",
                schema: "Inventory",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                schema: "Inventory",
                table: "Inventory");

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                schema: "Inventory",
                table: "Inventory",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Warehouse_WarehouseId",
                schema: "Inventory",
                table: "Inventory",
                column: "WarehouseId",
                principalSchema: "Inventory",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 🔁 Rename UnitPrice back to Price
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                schema: "User",
                table: "CartItem",
                newName: "Price");
          
            // 🔁 Drop new FK between Inventory and Warehouse
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Warehouse",
                schema: "Inventory",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
             name: "PK__stores__A2F2A30C9859C215",
             schema: "Inventory",
             table: "Warehouse");

            // 🔁 Drop the Guid WarehouseId columns
            migrationBuilder.DropColumn(
                name: "WarehouseId",
                schema: "Inventory",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                schema: "Inventory",
                table: "Warehouse");

            // 🔁 Recreate original WarehouseId columns as int
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                schema: "Inventory",
                table: "Warehouse",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                schema: "Inventory",
                table: "Inventory",
                type: "int",
                nullable: false);

            // 🔁 Restore FK constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Warehouse_WarehouseId",
                schema: "Inventory",
                table: "Inventory",
                column: "WarehouseId",
                principalSchema: "Inventory",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
