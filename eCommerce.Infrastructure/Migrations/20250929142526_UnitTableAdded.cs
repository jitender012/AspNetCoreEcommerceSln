using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UnitTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeasurementUnitId",
                schema: "Product",
                table: "ProductFeatures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MeasurementUnits",
                columns: table => new
                {
                    MeasurementUnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitSymbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UnitType = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnits", x => x.MeasurementUnitId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_MeasurementUnitId",
                schema: "Product",
                table: "ProductFeatures",
                column: "MeasurementUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_MeasurementUnits_MeasurementUnitId",
                schema: "Product",
                table: "ProductFeatures",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnits",
                principalColumn: "MeasurementUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_MeasurementUnits_MeasurementUnitId",
                schema: "Product",
                table: "ProductFeatures");

            migrationBuilder.DropTable(
                name: "MeasurementUnits");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatures_MeasurementUnitId",
                schema: "Product",
                table: "ProductFeatures");

            migrationBuilder.DropColumn(
                name: "MeasurementUnitId",
                schema: "Product",
                table: "ProductFeatures");
        }
    }
}
