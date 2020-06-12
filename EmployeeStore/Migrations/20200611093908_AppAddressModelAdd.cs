using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeStore.Migrations
{
    public partial class AppAddressModelAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppSocialAddress",
                table: "AppInfos");

            migrationBuilder.CreateTable(
                name: "AppSocialAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlAddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialAddresses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialAddresses");

            migrationBuilder.AddColumn<string>(
                name: "AppSocialAddress",
                table: "AppInfos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
