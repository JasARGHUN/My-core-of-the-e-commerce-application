using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeStore.Migrations
{
    public partial class AddApplicationData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Zip",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AppInfos",
                columns: table => new
                {
                    AppInfoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppName = table.Column<string>(nullable: false),
                    AppAddress = table.Column<string>(nullable: false),
                    AppPhone1 = table.Column<string>(nullable: false),
                    AppPhone2 = table.Column<string>(nullable: false),
                    AppDescription = table.Column<string>(nullable: false),
                    AppEmail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInfos", x => x.AppInfoID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppInfos");

            migrationBuilder.AlterColumn<string>(
                name: "Zip",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
