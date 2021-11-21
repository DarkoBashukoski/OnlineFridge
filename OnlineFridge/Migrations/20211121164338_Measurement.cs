using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineFridge.Migrations
{
    public partial class Measurement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "recipeName",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "recipeDesc",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeasurementID",
                table: "Quantity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Measurement",
                columns: table => new
                {
                    MeasurementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    measurementName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurement", x => x.MeasurementID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quantity_MeasurementID",
                table: "Quantity",
                column: "MeasurementID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Measurement_MeasurementID",
                table: "Quantity",
                column: "MeasurementID",
                principalTable: "Measurement",
                principalColumn: "MeasurementID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Measurement_MeasurementID",
                table: "Quantity");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropIndex(
                name: "IX_Quantity_MeasurementID",
                table: "Quantity");

            migrationBuilder.DropColumn(
                name: "MeasurementID",
                table: "Quantity");

            migrationBuilder.AlterColumn<string>(
                name: "recipeName",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "recipeDesc",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);
        }
    }
}
