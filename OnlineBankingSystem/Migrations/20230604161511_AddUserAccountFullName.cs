using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBankingSystem.Migrations
{
    public partial class AddUserAccountFullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TargetIBAN",
                table: "UserAccountActivity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TargetUserFullName",
                table: "UserAccountActivity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetIBAN",
                table: "UserAccountActivity");

            migrationBuilder.DropColumn(
                name: "TargetUserFullName",
                table: "UserAccountActivity");
        }
    }
}
