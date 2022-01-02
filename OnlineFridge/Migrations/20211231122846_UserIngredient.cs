using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineFridge.Migrations
{
    public partial class UserIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserID",
                table: "Recipe",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserIngredient",
                columns: table => new
                {
                    UserIngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIngredient", x => x.UserIngredientID);
                    table.ForeignKey(
                        name: "FK_UserIngredient_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserIngredient_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_ApplicationUserID",
                table: "Recipe",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserIngredient_ApplicationUserID",
                table: "UserIngredient",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserIngredient_IngredientID",
                table: "UserIngredient",
                column: "IngredientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_AspNetUsers_ApplicationUserID",
                table: "Recipe",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_AspNetUsers_ApplicationUserID",
                table: "Recipe");

            migrationBuilder.DropTable(
                name: "UserIngredient");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_ApplicationUserID",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "Recipe");
        }
    }
}
