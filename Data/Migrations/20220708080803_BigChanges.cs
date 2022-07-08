using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class BigChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignRequirement_Recruitments_RecruitmentId",
                table: "CampaignRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignRequirement_Skills_SkillId",
                table: "CampaignRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_RecruiterId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_TechId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitments_AspNetUsers_RecruiterId",
                table: "Recruitments");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recruitments",
                table: "Recruitments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CampaignRequirement",
                table: "CampaignRequirement");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "Skill");

            migrationBuilder.RenameTable(
                name: "Recruitments",
                newName: "Recruitment");

            migrationBuilder.RenameTable(
                name: "Candidates",
                newName: "Candidate");

            migrationBuilder.RenameTable(
                name: "CampaignRequirement",
                newName: "RecruitmentRequirement");

            migrationBuilder.RenameIndex(
                name: "IX_Recruitments_RecruiterId",
                table: "Recruitment",
                newName: "IX_Recruitment_RecruiterId");

            migrationBuilder.RenameIndex(
                name: "IX_Candidates_TechId",
                table: "Candidate",
                newName: "IX_Candidate_TechId");

            migrationBuilder.RenameIndex(
                name: "IX_Candidates_RecruiterId",
                table: "Candidate",
                newName: "IX_Candidate_RecruiterId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignRequirement_SkillId",
                table: "RecruitmentRequirement",
                newName: "IX_RecruitmentRequirement_SkillId");

            migrationBuilder.AlterColumn<int>(
                name: "RecruiterId",
                table: "Recruitment",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Recruitment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndedById",
                table: "Recruitment",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedById",
                table: "Recruitment",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Recruitment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TechInterviewDate",
                table: "Candidate",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "TechId",
                table: "Candidate",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "RecruiterId",
                table: "Candidate",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InterviewDate",
                table: "Candidate",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Candidate",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedById",
                table: "Candidate",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Candidate",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skill",
                table: "Skill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recruitment",
                table: "Recruitment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidate",
                table: "Candidate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecruitmentRequirement",
                table: "RecruitmentRequirement",
                columns: new[] { "RecruitmentId", "SkillId" });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_User_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_RecruitmentId",
                table: "Candidate",
                column: "RecruitmentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_DeletedById",
                table: "User",
                column: "DeletedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Recruitment_RecruitmentId",
                table: "Candidate",
                column: "RecruitmentId",
                principalTable: "Recruitment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_CreatedById",
                table: "Candidate",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Candidate_User_RecruiterId",
                table: "Candidate",
                column: "RecruiterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_TechId",
                table: "Candidate",
                column: "TechId",
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
                name: "FK_Recruitment_User_RecruiterId",
                table: "Recruitment",
                column: "RecruiterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentRequirement_Recruitment_RecruitmentId",
                table: "RecruitmentRequirement",
                column: "RecruitmentId",
                principalTable: "Recruitment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentRequirement_Skill_SkillId",
                table: "RecruitmentRequirement",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Recruitment_RecruitmentId",
                table: "Candidate");

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
                name: "FK_Candidate_User_RecruiterId",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_TechId",
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
                name: "FK_Recruitment_User_RecruiterId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentRequirement_Recruitment_RecruitmentId",
                table: "RecruitmentRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentRequirement_Skill_SkillId",
                table: "RecruitmentRequirement");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skill",
                table: "Skill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecruitmentRequirement",
                table: "RecruitmentRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recruitment",
                table: "Recruitment");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidate",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_CreatedById",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_DeletedById",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_LastUpdatedById",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_RecruitmentId",
                table: "Candidate");

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

            migrationBuilder.RenameTable(
                name: "Skill",
                newName: "Skills");

            migrationBuilder.RenameTable(
                name: "RecruitmentRequirement",
                newName: "CampaignRequirement");

            migrationBuilder.RenameTable(
                name: "Recruitment",
                newName: "Recruitments");

            migrationBuilder.RenameTable(
                name: "Candidate",
                newName: "Candidates");

            migrationBuilder.RenameIndex(
                name: "IX_RecruitmentRequirement_SkillId",
                table: "CampaignRequirement",
                newName: "IX_CampaignRequirement_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_Recruitment_RecruiterId",
                table: "Recruitments",
                newName: "IX_Recruitments_RecruiterId");

            migrationBuilder.RenameIndex(
                name: "IX_Candidate_TechId",
                table: "Candidates",
                newName: "IX_Candidates_TechId");

            migrationBuilder.RenameIndex(
                name: "IX_Candidate_RecruiterId",
                table: "Candidates",
                newName: "IX_Candidates_RecruiterId");

            migrationBuilder.AlterColumn<string>(
                name: "RecruiterId",
                table: "Recruitments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TechInterviewDate",
                table: "Candidates",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TechId",
                table: "Candidates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RecruiterId",
                table: "Candidates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InterviewDate",
                table: "Candidates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CampaignRequirement",
                table: "CampaignRequirement",
                columns: new[] { "RecruitmentId", "SkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recruitments",
                table: "Recruitments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignRequirement_Recruitments_RecruitmentId",
                table: "CampaignRequirement",
                column: "RecruitmentId",
                principalTable: "Recruitments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignRequirement_Skills_SkillId",
                table: "CampaignRequirement",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AspNetUsers_RecruiterId",
                table: "Candidates",
                column: "RecruiterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AspNetUsers_TechId",
                table: "Candidates",
                column: "TechId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitments_AspNetUsers_RecruiterId",
                table: "Recruitments",
                column: "RecruiterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
