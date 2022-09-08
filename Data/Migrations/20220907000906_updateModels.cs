using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectPlanningApp.Data.Migrations
{
    public partial class updateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardLists_Boards_BoardId",
                table: "BoardLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Boards_BoardId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_BoardId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "BoardLists",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardLists_Boards_BoardId",
                table: "BoardLists",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardLists_Boards_BoardId",
                table: "BoardLists");

            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "BoardLists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BoardId",
                table: "Cards",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardLists_Boards_BoardId",
                table: "BoardLists",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Boards_BoardId",
                table: "Cards",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
