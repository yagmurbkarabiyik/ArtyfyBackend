using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtyfyBackend.Dal.Migrations
{
    /// <inheritdoc />
    public partial class postImagesUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostImages_AspNetUsers_UserAppId",
                table: "PostImages");

            migrationBuilder.DropIndex(
                name: "IX_PostImages_UserAppId",
                table: "PostImages");

            migrationBuilder.DropColumn(
                name: "UserAppId",
                table: "PostImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAppId",
                table: "PostImages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PostImages_UserAppId",
                table: "PostImages",
                column: "UserAppId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostImages_AspNetUsers_UserAppId",
                table: "PostImages",
                column: "UserAppId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
