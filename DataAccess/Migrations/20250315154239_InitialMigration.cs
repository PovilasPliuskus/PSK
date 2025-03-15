using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FkCreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workspace_User_FkCreatedByUserId",
                        column: x => x.FkCreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FkCreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkWorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkAssignedToUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Estimate = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_User_FkAssignedToUserId",
                        column: x => x.FkAssignedToUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Task_User_FkCreatedByUserId",
                        column: x => x.FkCreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Workspace_FkWorkspaceId",
                        column: x => x.FkWorkspaceId,
                        principalTable: "Workspace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkspaceUsers",
                columns: table => new
                {
                    FkUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkWorkspaceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceUsers", x => new { x.FkUserId, x.FkWorkspaceId });
                    table.ForeignKey(
                        name: "FK_WorkspaceUsers_User_FkUserId",
                        column: x => x.FkUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkspaceUsers_Workspace_FkWorkspaceId",
                        column: x => x.FkWorkspaceId,
                        principalTable: "Workspace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FkTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkCreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkAssignedToUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Estimate = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTask_Task_FkTaskId",
                        column: x => x.FkTaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubTask_User_FkAssignedToUserId",
                        column: x => x.FkAssignedToUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SubTask_User_FkCreatedByUserId",
                        column: x => x.FkCreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FkCreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkTaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    FkSubTaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_SubTask_FkSubTaskId",
                        column: x => x.FkSubTaskId,
                        principalTable: "SubTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attachment_Task_FkTaskId",
                        column: x => x.FkTaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attachment_User_FkCreatedByUserId",
                        column: x => x.FkCreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FkTaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    FkSubTaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    FkWrittenByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Edited = table.Column<bool>(type: "boolean", nullable: false),
                    Text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_SubTask_FkSubTaskId",
                        column: x => x.FkSubTaskId,
                        principalTable: "SubTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Task_FkTaskId",
                        column: x => x.FkTaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_User_FkWrittenByUserId",
                        column: x => x.FkWrittenByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_FkCreatedByUserId",
                table: "Attachment",
                column: "FkCreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_FkSubTaskId",
                table: "Attachment",
                column: "FkSubTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_FkTaskId",
                table: "Attachment",
                column: "FkTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FkSubTaskId",
                table: "Comment",
                column: "FkSubTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FkTaskId",
                table: "Comment",
                column: "FkTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FkWrittenByUserId",
                table: "Comment",
                column: "FkWrittenByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTask_FkAssignedToUserId",
                table: "SubTask",
                column: "FkAssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTask_FkCreatedByUserId",
                table: "SubTask",
                column: "FkCreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTask_FkTaskId",
                table: "SubTask",
                column: "FkTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_FkAssignedToUserId",
                table: "Task",
                column: "FkAssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_FkCreatedByUserId",
                table: "Task",
                column: "FkCreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_FkWorkspaceId",
                table: "Task",
                column: "FkWorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_StudentId",
                table: "User",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workspace_FkCreatedByUserId",
                table: "Workspace",
                column: "FkCreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceUsers_FkWorkspaceId",
                table: "WorkspaceUsers",
                column: "FkWorkspaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "WorkspaceUsers");

            migrationBuilder.DropTable(
                name: "SubTask");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Workspace");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
