using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.CreateTable(
                name: "StockHistory",
                schema: "Inventory",
                columns: table => new
                {
                    StockHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductVariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreviousQty = table.Column<int>(type: "int", nullable: false),
                    ChangedQty = table.Column<int>(type: "int", nullable: false),
                    NewQty = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ActionReason = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHistory", x => x.StockHistoryId);
                    table.ForeignKey(
                        name: "FK_StockHistory_ProductVarient",
                        column: x => x.ProductVariantId,
                        principalSchema: "Product",
                        principalTable: "ProductVariant",
                        principalColumn: "ProductIVarientId");
                    table.ForeignKey(
                        name: "FK_StockHistory_Warehouse",
                        column: x => x.WarehouseId,
                        principalSchema: "Inventory",
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockHistory_ProductVariantId",
                schema: "Inventory",
                table: "StockHistory",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistory_WarehouseId",
                schema: "Inventory",
                table: "StockHistory",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockHistory",
                schema: "Inventory");
         
        }
    }
}
