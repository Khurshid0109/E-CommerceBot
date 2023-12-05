using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramBot.Data.Migrations
{
    /// <inheritdoc />
    public partial class VerificationStepAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VerificationStep",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationStep",
                table: "Users");
        }
    }
}
