using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureFMSDB.Migrations
{
    public partial class fixreplytble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replie_Feedback_FeedbackId",
                table: "Replie");

            migrationBuilder.DropForeignKey(
                name: "FK_Replie_User_UserId",
                table: "Replie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Replie",
                table: "Replie");

            migrationBuilder.RenameTable(
                name: "Replie",
                newName: "Reply");

            migrationBuilder.RenameIndex(
                name: "IX_Replie_UserId",
                table: "Reply",
                newName: "IX_Reply_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Replie_FeedbackId",
                table: "Reply",
                newName: "IX_Reply_FeedbackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reply",
                table: "Reply",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_Feedback_FeedbackId",
                table: "Reply",
                column: "FeedbackId",
                principalTable: "Feedback",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_User_UserId",
                table: "Reply",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reply_Feedback_FeedbackId",
                table: "Reply");

            migrationBuilder.DropForeignKey(
                name: "FK_Reply_User_UserId",
                table: "Reply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reply",
                table: "Reply");

            migrationBuilder.RenameTable(
                name: "Reply",
                newName: "Replie");

            migrationBuilder.RenameIndex(
                name: "IX_Reply_UserId",
                table: "Replie",
                newName: "IX_Replie_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reply_FeedbackId",
                table: "Replie",
                newName: "IX_Replie_FeedbackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Replie",
                table: "Replie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Replie_Feedback_FeedbackId",
                table: "Replie",
                column: "FeedbackId",
                principalTable: "Feedback",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replie_User_UserId",
                table: "Replie",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
