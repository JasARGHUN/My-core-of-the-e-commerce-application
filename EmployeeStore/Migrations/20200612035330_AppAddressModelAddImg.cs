using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeStore.Migrations
{
    public partial class AppAddressModelAddImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppSocialImg",
                table: "AppSocialAddresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppSocialImg",
                table: "AppSocialAddresses");
        }
    }
}
