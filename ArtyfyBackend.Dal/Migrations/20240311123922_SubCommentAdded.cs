using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtyfyBackend.Dal.Migrations
{
    /// <inheritdoc />
    public partial class SubCommentAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserAppId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "UserAppId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "SubComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubComments_CommentId",
                table: "SubComments",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserAppId",
                table: "Comments",
                column: "UserAppId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserAppId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "SubComments");

            migrationBuilder.AlterColumn<string>(
                name: "UserAppId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserAppId",
                table: "Comments",
                column: "UserAppId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
