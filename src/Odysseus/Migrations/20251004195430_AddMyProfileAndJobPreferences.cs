﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Odysseus.Host.Migrations
{
    /// <inheritdoc />
    public partial class AddMyProfileAndJobPreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Passport = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NeedRelocationSupport = table.Column<bool>(type: "boolean", nullable: true),
                    NeedSponsorship = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyJobPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    MyProfileId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    WorkModel = table.Column<int>(type: "integer", nullable: false),
                    Contract = table.Column<int>(type: "integer", nullable: false),
                    OfferRelocation = table.Column<bool>(type: "boolean", nullable: false),
                    OfferSponsorship = table.Column<bool>(type: "boolean", nullable: false),
                    TotalCompensation = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyJobPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyJobPreferences_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyJobPreferences_MyProfiles_MyProfileId",
                        column: x => x.MyProfileId,
                        principalTable: "MyProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyJobPreferences_MyProfileId",
                table: "MyJobPreferences",
                column: "MyProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MyJobPreferences_UserId",
                table: "MyJobPreferences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MyJobPreferences_UserId_Title",
                table: "MyJobPreferences",
                columns: new[] { "UserId", "Title" });

            migrationBuilder.CreateIndex(
                name: "IX_MyProfiles_UserId",
                table: "MyProfiles",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyJobPreferences");

            migrationBuilder.DropTable(
                name: "MyProfiles");
        }
    }
}
