using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removed_smartphone_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartPhone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartPhone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    colour = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dimension = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    model_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    network_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    primary_camera = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    processor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    ram = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    screen_size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    secondary_camera = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    storage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    weight = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartPhone", x => x.Id);
                });
        }
    }
}
