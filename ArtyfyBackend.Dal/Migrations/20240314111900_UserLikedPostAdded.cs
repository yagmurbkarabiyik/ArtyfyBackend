using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtyfyBackend.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UserLikedPostAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLikedPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAppId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikedPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLikedPosts_AspNetUsers_UserAppId",
                        column: x => x.UserAppId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLikedPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedPosts_PostId",
                table: "UserLikedPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedPosts_UserAppId",
                table: "UserLikedPosts",
                column: "UserAppId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLikedPosts");
        }
    }
}
