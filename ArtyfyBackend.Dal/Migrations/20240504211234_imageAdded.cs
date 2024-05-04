using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtyfyBackend.Dal.Migrations
{
    /// <inheritdoc />
    public partial class imageAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "PostImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "PostImages");
        }
    }
}
