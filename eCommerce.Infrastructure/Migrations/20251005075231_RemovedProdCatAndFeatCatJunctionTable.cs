using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedProdCatAndFeatCatJunctionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategoryFeature",
                schema: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategoryFeature",
                schema: "Product",
                columns: table => new
                {
                    ProductCategoryFeatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariationCategory", x => x.ProductCategoryFeatureId);
                    table.ForeignKey(
                        name: "FK_ProductCategoryFeature_FeatureCategory",
                        column: x => x.FeatureCategoryId,
                        principalSchema: "Product",
                        principalTable: "FeatureCategory",
                        principalColumn: "FeatureCategoryId");
                    table.ForeignKey(
                        name: "FK_ProductCategoryFeature_ProductCategory",
                        column: x => x.ProductCategoryId,
                        principalSchema: "Marketing",
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryFeature_FeatureCategoryId_ProductCategoryId",
                schema: "Product",
                table: "ProductCategoryFeature",
                columns: new[] { "FeatureCategoryId", "ProductCategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryFeature_ProductCategoryId",
                schema: "Product",
                table: "ProductCategoryFeature",
                column: "ProductCategoryId");
        }
    }
}
