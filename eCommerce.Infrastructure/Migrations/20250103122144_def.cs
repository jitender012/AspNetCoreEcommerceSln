using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Def : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "Marketing",
                table: "Category",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Marketing",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Marketing",
                table: "Category",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Marketing",
                table: "Category",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                schema: "Marketing",
                table: "Category",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "Marketing",
                table: "Category",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Marketing",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Marketing",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Marketing",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Marketing",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "Marketing",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "Marketing",
                table: "Category");
        }
    }
}
