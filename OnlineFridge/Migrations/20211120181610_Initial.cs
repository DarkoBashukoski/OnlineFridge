using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineFridge.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    IngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ingredientName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.IngredientID);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    foodCategory = table.Column<int>(type: "int", nullable: false),
                    recipeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recipeDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prepTime = table.Column<int>(type: "int", nullable: false),
                    cookTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.RecipeID);
                });

            migrationBuilder.CreateTable(
                name: "Quantity",
                columns: table => new
                {
                    QuantityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quantity", x => x.QuantityID);
                    table.ForeignKey(
                        name: "FK_Quantity_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quantity_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Step",
                columns: table => new
                {
                    StepID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    stepNumber = table.Column<int>(type: "int", nullable: false),
                    stepDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => x.StepID);
                    table.ForeignKey(
                        name: "FK_Step_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quantity_IngredientID",
                table: "Quantity",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_Quantity_RecipeID",
                table: "Quantity",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Step_RecipeID",
                table: "Step",
                column: "RecipeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quantity");

            migrationBuilder.DropTable(
                name: "Step");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Recipe");
        }
    }
}
