using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineFridge.Migrations
{
    public partial class DeleteMeasurements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
