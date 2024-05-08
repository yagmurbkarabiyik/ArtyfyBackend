﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtyfyBackend.Dal.Migrations
{
    /// <inheritdoc />
    public partial class postTablePriceAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Posts",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Posts");
        }
    }
}
