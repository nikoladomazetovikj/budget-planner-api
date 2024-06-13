using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace budget_planner_api.Migrations
{
    /// <inheritdoc />
    public partial class add_missing_price_column_tobudgets_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Budgets",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Budgets");
        }
    }
}
