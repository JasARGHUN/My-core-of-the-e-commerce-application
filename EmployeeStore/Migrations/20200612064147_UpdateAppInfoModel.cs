using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeStore.Migrations
{
    public partial class UpdateAppInfoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppSocialImg",
                table: "AppInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppSocialImg",
                table: "AppInfos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
