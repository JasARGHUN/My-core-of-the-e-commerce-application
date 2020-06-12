using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeStore.Migrations
{
    public partial class AddAppSocialData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppImage",
                table: "AppInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppSocialAddress",
                table: "AppInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppSocialImg",
                table: "AppInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppImage",
                table: "AppInfos");

            migrationBuilder.DropColumn(
                name: "AppSocialAddress",
                table: "AppInfos");

            migrationBuilder.DropColumn(
                name: "AppSocialImg",
                table: "AppInfos");
        }
    }
}
