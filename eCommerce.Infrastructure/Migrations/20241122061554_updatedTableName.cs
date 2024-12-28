using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MainCategory",
                schema: "production",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_MainCategory",
                schema: "production",
                table: "Products");

            migrationBuilder.DropTable(
                name: "MainCategory");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MainCategoryId",
                schema: "production",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "MainCategoryId",
                schema: "production",
                table: "Categories",
                newName: "SubCategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                schema: "production",
                table: "Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CategoryImage = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MainCategoryId",
                schema: "production",
                table: "Categories",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MainCategory",
                schema: "production",
                table: "Categories",
                column: "category_id",
                principalTable: "Category",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MainCategory",
                schema: "production",
                table: "Products",
                column: "main_category_id",
                principalTable: "Category",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MainCategory",
                schema: "production",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_MainCategory",
                schema: "production",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MainCategoryId",
                schema: "production",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                schema: "production",
                table: "Categories",
                newName: "MainCategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                schema: "production",
                table: "Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "MainCategory",
                columns: table => new
                {
                    MainCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainCategoryImage = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    MainCategoryName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategory", x => x.MainCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MainCategoryId",
                schema: "production",
                table: "Categories",
                column: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MainCategory",
                schema: "production",
                table: "Categories",
                column: "MainCategoryId",
                principalTable: "MainCategory",
                principalColumn: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MainCategory",
                schema: "production",
                table: "Products",
                column: "main_category_id",
                principalTable: "MainCategory",
                principalColumn: "MainCategoryId");
        }
    }
}
