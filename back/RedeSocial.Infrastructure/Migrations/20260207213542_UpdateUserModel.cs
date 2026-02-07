using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedeSocial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Friends_UserId",
                table: "Friends");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UserId_FriendUserId",
                table: "Friends",
                columns: new[] { "UserId", "FriendUserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Friends_UserId_FriendUserId",
                table: "Friends");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UserId",
                table: "Friends",
                column: "UserId");
        }
    }
}
