using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SponsorshipWorkflow.Migrations
{
    /// <inheritdoc />
    public partial class added_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSponsorshipTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSponsorshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSponsorshipRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    RequestorName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SponsorshipTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    EventDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RequestedAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Purpose = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ExpectedBusinessBenefit = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ManagerRemarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FinanceRemarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSponsorshipRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSponsorshipRequests_AppSponsorshipTypes_SponsorshipTypeId",
                        column: x => x.SponsorshipTypeId,
                        principalTable: "AppSponsorshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppWorkflowHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SponsorshipRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    Action = table.Column<int>(type: "integer", nullable: false),
                    PreviousStatus = table.Column<int>(type: "integer", nullable: false),
                    NewStatus = table.Column<int>(type: "integer", nullable: false),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PerformedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    PerformedByUserName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PerformedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorkflowHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWorkflowHistories_AppSponsorshipRequests_SponsorshipRequ~",
                        column: x => x.SponsorshipRequestId,
                        principalTable: "AppSponsorshipRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSponsorshipRequests_SponsorshipTypeId",
                table: "AppSponsorshipRequests",
                column: "SponsorshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSponsorshipRequests_Status",
                table: "AppSponsorshipRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkflowHistories_SponsorshipRequestId",
                table: "AppWorkflowHistories",
                column: "SponsorshipRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppWorkflowHistories");

            migrationBuilder.DropTable(
                name: "AppSponsorshipRequests");

            migrationBuilder.DropTable(
                name: "AppSponsorshipTypes");
        }
    }
}
