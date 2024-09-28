using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawFund.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addRelationRoleUser_Account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
