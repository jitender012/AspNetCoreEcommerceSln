using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetFeatureOptionCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureOption_ProductFeatures",
                schema: "Product",
                table: "FeatureOption");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureOption_ProductFeatures",
                schema: "Product",
                table: "FeatureOption",
                column: "ProductFeatureId",
                principalSchema: "Product",
                principalTable: "ProductFeatures",
                principalColumn: "ProductFeaturesId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureOption_ProductFeatures",
                schema: "Product",
                table: "FeatureOption");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureOption_ProductFeatures",
                schema: "Product",
                table: "FeatureOption",
                column: "ProductFeatureId",
                principalSchema: "Product",
                principalTable: "ProductFeatures",
                principalColumn: "ProductFeaturesId");
        }
    }
}
