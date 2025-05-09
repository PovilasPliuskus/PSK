using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SubtaskAndCommentFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_SubTask_SubTaskEntityId",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Task_TaskEntityId",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_SubTaskEntityId",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_TaskEntityId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "SubTaskEntityId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "TaskEntityId",
                table: "Attachment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubTaskEntityId",
                table: "Attachment",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskEntityId",
                table: "Attachment",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_SubTaskEntityId",
                table: "Attachment",
                column: "SubTaskEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_TaskEntityId",
                table: "Attachment",
                column: "TaskEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_SubTask_SubTaskEntityId",
                table: "Attachment",
                column: "SubTaskEntityId",
                principalTable: "SubTask",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Task_TaskEntityId",
                table: "Attachment",
                column: "TaskEntityId",
                principalTable: "Task",
                principalColumn: "Id");
        }
    }
}
