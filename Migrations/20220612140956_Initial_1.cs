using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectDipMVC.Migrations
{
    public partial class Initial_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    login = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    password = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Project_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Date_create = table.Column<DateTime>(type: "date", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    Titul_Name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Titul_File = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Project_id);
                    table.ForeignKey(
                        name: "FK_Projects_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Project_Descript",
                columns: table => new
                {
                    Proj_Dscrpt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Section_Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Section_Number = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    Project_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proj_Dscrpt", x => x.Proj_Dscrpt_id);
                    table.ForeignKey(
                        name: "FK_Proj_Dscrpt_Project",
                        column: x => x.Project_id,
                        principalTable: "Projects",
                        principalColumn: "Project_id");
                    table.ForeignKey(
                        name: "FK_Proj_Dscrpt_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sections_Project",
                columns: table => new
                {
                    Sections_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_sections = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Number_sections = table.Column<int>(type: "int", nullable: true),
                    Proj_Dscrpt_id = table.Column<int>(type: "int", nullable: true),
                    Name_File_sections = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    File_sections = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections_Project", x => x.Sections_id);
                    table.ForeignKey(
                        name: "FK_Proj_Dscrpt",
                        column: x => x.Proj_Dscrpt_id,
                        principalTable: "Project_Descript",
                        principalColumn: "Proj_Dscrpt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_Descript_Project_id",
                table: "Project_Descript",
                column: "Project_id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Descript_user_id",
                table: "Project_Descript",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Project___E1B3E8144235C593",
                table: "Project_Descript",
                columns: new[] { "Section_Number", "Project_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_user_id",
                table: "Projects",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Project_Proj_Dscrpt_id",
                table: "Sections_Project",
                column: "Proj_Dscrpt_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Sections__548CC8E751DBDC58",
                table: "Sections_Project",
                columns: new[] { "Number_sections", "Proj_Dscrpt_id" },
                unique: true,
                filter: "[Number_sections] IS NOT NULL AND [Proj_Dscrpt_id] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sections_Project");

            migrationBuilder.DropTable(
                name: "Project_Descript");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
