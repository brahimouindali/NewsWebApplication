using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Access.Library.Migrations
{
    public partial class addIsVisibleToAticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Articles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Articles");
        }
    }
}
