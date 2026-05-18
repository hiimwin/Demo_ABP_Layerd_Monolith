using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace abpSourceCode.Migrations
{
    /// <inheritdoc />
    public partial class Added_Category_Extra_Fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AppCategories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "AppCategories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AppCategories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AppCategories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "AppCategories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "AppCategories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "AppCategories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "AppCategories");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "AppCategories");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AppCategories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AppCategories");

            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "AppCategories");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "AppCategories");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "AppCategories");
        }
    }
}
