using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductFeaturesIdFromPCF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoryFeature_ProductFeatures",
                schema: "Product",
                table: "ProductCategoryFeature");

          
            migrationBuilder.DropColumn(
                name: "ProductFeaturesId",
                schema: "Product",
                table: "ProductCategoryFeature");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductFeaturesId",
                schema: "Product",
                table: "ProductCategoryFeature",
                type: "int",
                nullable: true);

           

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoryFeature_ProductFeatures",
                schema: "Product",
                table: "ProductCategoryFeature",
                column: "ProductFeaturesId",
                principalSchema: "Product",
                principalTable: "ProductFeatures",
                principalColumn: "ProductFeaturesId");
        }
    }
}
