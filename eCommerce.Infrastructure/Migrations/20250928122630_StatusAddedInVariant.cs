using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatusAddedInVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Product",
                table: "ProductVariant");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Product",
                table: "ProductVariant",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Product",
                table: "ProductVariant");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Product",
                table: "ProductVariant",
                type: "bit",
                nullable: true);
        }
    }
}
