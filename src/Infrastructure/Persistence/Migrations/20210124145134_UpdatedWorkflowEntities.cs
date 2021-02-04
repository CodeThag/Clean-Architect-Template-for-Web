using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdatedWorkflowEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanBeInlined",
                table: "WorkflowScheme",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InlinedSchemes",
                table: "WorkflowScheme",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "WorkflowScheme",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanBeInlined",
                table: "WorkflowScheme");

            migrationBuilder.DropColumn(
                name: "InlinedSchemes",
                table: "WorkflowScheme");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "WorkflowScheme");
        }
    }
}
