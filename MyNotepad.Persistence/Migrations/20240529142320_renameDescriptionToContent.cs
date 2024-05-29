using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyNotepad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class renameDescriptionToContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "notes",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "notes",
                newName: "Description");
        }
    }
}
