﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Api.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserIdToRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Recipes");
        }
    }
}
