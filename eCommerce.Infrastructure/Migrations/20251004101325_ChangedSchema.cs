using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_MeasurementUnits_MeasurementUnitId",
                schema: "Product",
                table: "ProductFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasurementUnits",
                table: "MeasurementUnits");

            migrationBuilder.RenameTable(
                name: "ProductCategoryProductFeature",
                newName: "ProductCategoryProductFeature",
                newSchema: "Product");

            migrationBuilder.RenameTable(
                name: "FeatureCategory",
                newName: "FeatureCategory",
                newSchema: "Product");

            migrationBuilder.RenameTable(
                name: "MeasurementUnits",
                newName: "MeasurementUnit",
                newSchema: "Product");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasurementUnit",
                schema: "Product",
                table: "MeasurementUnit",
                column: "MeasurementUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_MeasurementUnit_MeasurementUnitId",
                schema: "Product",
                table: "ProductFeatures",
                column: "MeasurementUnitId",
                principalSchema: "Product",
                principalTable: "MeasurementUnit",
                principalColumn: "MeasurementUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_MeasurementUnit_MeasurementUnitId",
                schema: "Product",
                table: "ProductFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasurementUnit",
                schema: "Product",
                table: "MeasurementUnit");

            migrationBuilder.RenameTable(
                name: "ProductCategoryProductFeature",
                schema: "Product",
                newName: "ProductCategoryProductFeature");

            migrationBuilder.RenameTable(
                name: "FeatureCategory",
                schema: "Product",
                newName: "FeatureCategory");

            migrationBuilder.RenameTable(
                name: "MeasurementUnit",
                schema: "Product",
                newName: "MeasurementUnits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasurementUnits",
                table: "MeasurementUnits",
                column: "MeasurementUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_MeasurementUnits_MeasurementUnitId",
                schema: "Product",
                table: "ProductFeatures",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnits",
                principalColumn: "MeasurementUnitId");
        }
    }
}
