using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToProductCategoryFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_ProductCategoryFeature_FeatureCategoryId",
            //    schema: "Product",
            //    table: "ProductCategoryFeature");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryFeature_FeatureCategoryId_ProductCategoryId",
                schema: "Product",
                table: "ProductCategoryFeature",
                columns: new[] { "FeatureCategoryId", "ProductCategoryId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductCategoryFeature_FeatureCategoryId_ProductCategoryId",
                schema: "Product",
                table: "ProductCategoryFeature");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductCategoryFeature_FeatureCategoryId",
            //    schema: "Product",
            //    table: "ProductCategoryFeature",
            //    column: "FeatureCategoryId");
        }
    }
}
