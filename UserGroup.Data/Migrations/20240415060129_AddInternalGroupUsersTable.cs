using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserGroup.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInternalGroupUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalUserGroupUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InternalUserGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalUserGroupUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalUserGroupUsers_InternalUserGroup_InternalUserGroupId",
                        column: x => x.InternalUserGroupId,
                        principalTable: "InternalUserGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalUserGroupUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalUserGroupUsers_InternalUserGroupId",
                table: "InternalUserGroupUsers",
                column: "InternalUserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalUserGroupUsers_UserId",
                table: "InternalUserGroupUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalUserGroupUsers");
        }
    }
}
