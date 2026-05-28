using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitizenPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Commentid",
                table: "Comments",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationType",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationType",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "Commentid");
        }
    }
}
