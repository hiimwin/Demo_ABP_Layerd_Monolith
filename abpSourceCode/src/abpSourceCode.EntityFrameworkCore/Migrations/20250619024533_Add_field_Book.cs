using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace abpSourceCode.Migrations
{
    /// <inheritdoc />
    public partial class Add_field_Book : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppBooks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppBooks");
        }
    }
}
