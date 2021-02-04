using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdateEntityOrganisationTypesApplicationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationTypeOrganisationType",
                columns: table => new
                {
                    ApplicationTypesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganisationTypesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTypeOrganisationType", x => new { x.ApplicationTypesId, x.OrganisationTypesId });
                    table.ForeignKey(
                        name: "FK_ApplicationTypeOrganisationType_ApplicationTypes_ApplicationTypesId",
                        column: x => x.ApplicationTypesId,
                        principalTable: "ApplicationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationTypeOrganisationType_OrganisationTypes_OrganisationTypesId",
                        column: x => x.OrganisationTypesId,
                        principalTable: "OrganisationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendSystemNotification = table.Column<bool>(type: "bit", nullable: false),
                    EmailTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendEmailTemplate = table.Column<bool>(type: "bit", nullable: false),
                    SMSTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendSMSTemplate = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Recipient = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sender = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemNotifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTypeOrganisationType_OrganisationTypesId",
                table: "ApplicationTypeOrganisationType",
                column: "OrganisationTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_SystemName",
                table: "NotificationTemplates",
                column: "SystemName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationTypeOrganisationType");

            migrationBuilder.DropTable(
                name: "NotificationTemplates");

            migrationBuilder.DropTable(
                name: "SystemNotifications");
        }
    }
}
