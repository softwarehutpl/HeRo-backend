using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddForeignKeys2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedById",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Recruitment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Recruitment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "Recruitment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Recruitment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EndedById",
                table: "Recruitment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedById",
                table: "Recruitment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Recruitment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Candidate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Candidate",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "Candidate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Candidate",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedById",
                table: "Candidate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Candidate",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedById",
                table: "User",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_DeletedById",
                table: "User",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_LastUpdatedById",
                table: "User",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_CreatedById",
                table: "Recruitment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_DeletedById",
                table: "Recruitment",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_EndedById",
                table: "Recruitment",
                column: "EndedById");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_LastUpdatedById",
                table: "Recruitment",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_CreatedById",
                table: "Candidate",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_DeletedById",
                table: "Candidate",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_LastUpdatedById",
                table: "Candidate",
                column: "LastUpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_CreatedById",
                table: "Candidate",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_DeletedById",
                table: "Candidate",
                column: "DeletedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_LastUpdatedById",
                table: "Candidate",
                column: "LastUpdatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_User_CreatedById",
                table: "Recruitment",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_User_DeletedById",
                table: "Recruitment",
                column: "DeletedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_User_EndedById",
                table: "Recruitment",
                column: "EndedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_User_LastUpdatedById",
                table: "Recruitment",
                column: "LastUpdatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_CreatedById",
                table: "User",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_DeletedById",
                table: "User",
                column: "DeletedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_LastUpdatedById",
                table: "User",
                column: "LastUpdatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_CreatedById",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_DeletedById",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_LastUpdatedById",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_CreatedById",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_DeletedById",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_EndedById",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_LastUpdatedById",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_CreatedById",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_DeletedById",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_LastUpdatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CreatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_DeletedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_LastUpdatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Recruitment_CreatedById",
                table: "Recruitment");

            migrationBuilder.DropIndex(
                name: "IX_Recruitment_DeletedById",
                table: "Recruitment");

            migrationBuilder.DropIndex(
                name: "IX_Recruitment_EndedById",
                table: "Recruitment");

            migrationBuilder.DropIndex(
                name: "IX_Recruitment_LastUpdatedById",
                table: "Recruitment");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_CreatedById",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_DeletedById",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_LastUpdatedById",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "EndedById",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Candidate");
        }
    }
}
