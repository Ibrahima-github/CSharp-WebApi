using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebAPI.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PostName = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: true),
                    ArticleUploadDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PostDescription = table.Column<string>(type: "text", nullable: true),
                    PostYoutubeHref = table.Column<string>(type: "text", nullable: true),
                    AdsTitle = table.Column<string>(type: "text", nullable: true),
                    AdsImageFileName = table.Column<string>(type: "text", nullable: true),
                    AdsLink = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    UtilisateurId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UtilisateurUsername = table.Column<string>(type: "text", nullable: true),
                    UtilisateurEmailAddress = table.Column<string>(type: "text", nullable: true),
                    UtilisateurPassword = table.Column<string>(type: "text", nullable: true),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.UtilisateurId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
