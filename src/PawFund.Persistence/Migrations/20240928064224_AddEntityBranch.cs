using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawFund.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Branchs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumberOfBranch = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EmailOfBranch = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberHome = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    District = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branchs_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleUserId",
                table: "Accounts",
                column: "RoleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Branchs_AccountId",
                table: "Branchs",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_RoleUsers_RoleUserId",
                table: "Accounts",
                column: "RoleUserId",
                principalTable: "RoleUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_RoleUsers_RoleUserId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Branchs");

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
    }
}
