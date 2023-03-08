using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SurveyApp.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    Topic = table.Column<string>(type: "text", nullable: true),
                    CreatedDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Topic = table.Column<string>(type: "text", nullable: true),
                    StartTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<byte>(type: "smallint", nullable: false),
                    OwnerId = table.Column<string>(type: "text", nullable: true),
                    CreatedDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionnaires_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireItems",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    QuestionnaireId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionnaireItems_Questionnaires_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalSchema: "public",
                        principalTable: "Questionnaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireSubmissions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    QuestionnaireId = table.Column<int>(type: "integer", nullable: false),
                    ParticipantId = table.Column<string>(type: "text", nullable: true),
                    CreatedDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSubmissions_Questionnaires_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalSchema: "public",
                        principalTable: "Questionnaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSubmissions_Users_ParticipantId",
                        column: x => x.ParticipantId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireAnswers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AnswerValue = table.Column<string>(type: "text", nullable: true),
                    QuestionnaireItemId = table.Column<int>(type: "integer", nullable: false),
                    QuestionnaireSubmissionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionnaireAnswers_QuestionnaireItems_QuestionnaireItemId",
                        column: x => x.QuestionnaireItemId,
                        principalSchema: "public",
                        principalTable: "QuestionnaireItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireAnswers_QuestionnaireSubmissions_Questionnaire~",
                        column: x => x.QuestionnaireSubmissionId,
                        principalSchema: "public",
                        principalTable: "QuestionnaireSubmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireAnswers_QuestionnaireItemId",
                schema: "public",
                table: "QuestionnaireAnswers",
                column: "QuestionnaireItemId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireAnswers_QuestionnaireSubmissionId",
                schema: "public",
                table: "QuestionnaireAnswers",
                column: "QuestionnaireSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireItems_QuestionnaireId",
                schema: "public",
                table: "QuestionnaireItems",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_OwnerId",
                schema: "public",
                table: "Questionnaires",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSubmissions_ParticipantId",
                schema: "public",
                table: "QuestionnaireSubmissions",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSubmissions_QuestionnaireId",
                schema: "public",
                table: "QuestionnaireSubmissions",
                column: "QuestionnaireId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionnaireAnswers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "QuestionnaireItems",
                schema: "public");

            migrationBuilder.DropTable(
                name: "QuestionnaireSubmissions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Questionnaires",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "public");
        }
    }
}
