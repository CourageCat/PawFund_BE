using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawFund.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_RoleUsers_RoleUserId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_RoleUserId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "RoleUserId",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleId",
                table: "Accounts",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_RoleUsers_RoleId",
                table: "Accounts",
                column: "RoleId",
                principalTable: "RoleUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_RoleUsers_RoleId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_RoleId",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "RoleUserId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleUserId",
                table: "Accounts",
                column: "RoleUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_RoleUsers_RoleUserId",
                table: "Accounts",
                column: "RoleUserId",
                principalTable: "RoleUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
