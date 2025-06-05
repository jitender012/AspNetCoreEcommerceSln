using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewFeatureCatIdInProdFeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeatureCategoryId",
                schema: "Product",
                table: "ProductFeatures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_FeatureCategoryId",
                schema: "Product",
                table: "ProductFeatures",
                column: "FeatureCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_FeatureCategory_FeatureCategoryId",
                schema: "Product",
                table: "ProductFeatures",
                column: "FeatureCategoryId",
                principalTable: "FeatureCategory",
                principalColumn: "FeatureCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_FeatureCategory_FeatureCategoryId",
                schema: "Product",
                table: "ProductFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatures_FeatureCategoryId",
                schema: "Product",
                table: "ProductFeatures");

            migrationBuilder.DropColumn(
                name: "FeatureCategoryId",
                schema: "Product",
                table: "ProductFeatures");
        }
    }
}
