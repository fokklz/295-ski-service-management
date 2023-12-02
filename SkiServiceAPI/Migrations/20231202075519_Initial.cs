using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkiServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginAttempts = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "Id", "Days", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, 12, false, "Tief" },
                    { 2, 7, false, "Standard" },
                    { 3, 5, false, "Express" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "IsDeleted", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Belag-Vorschliff und Belag-Strukturschliff für Ski, einschließlich Plan-Schliff, Diamant-Steinschliff und Wachsen & Polieren für optimale Gleitfähigkeit und Steuerung auf verschiedenen Schneebedingungen.", false, "Kleiner Service", 49 },
                    { 2, "Planschleifen für präzise ebene Ski-Kanten und Belag, maschinelles Kanten-Schleifen für scharfe Kanten, Belagaufbesserung, Belag-Vorschliff, Belag-Strukturschliff und Entgraten zur Optimierung von Gleitverhalten, Lenkung und Haltbarkeit der Ski.", false, "Grosser Service", 69 },
                    { 3, "Planschleifen, maschinellem Kanten-Schleifen und Belagaufbesserung für maximale Geschwindigkeit und Präzision. Fortschrittliches Weltcup-Wachs für höchste Gleiteigenschaften und Langlebigkeit. Entgraten und Handfinish für optimale Skioberfläche und Wettkampfqualität.", false, "Rennski-Service", 99 },
                    { 4, "Professionelle Montage und Einstellung von Ski-Bindungen durch Expertenteam für maximale Sicherheit und Performance. Präzise Montage und individuelle Einstellung für optimalen Halt, schnelles Ansprechen bei Stürzen und Anpassung an den Fahrstil.", false, "Bindung montieren und einstellen", 39 },
                    { 5, "Maßgeschneiderte Skitouren-Felle, angepasst durch unser Fachteam für optimalen Grip und geschmeidiges Gleiten. Sorgfältiges Zuschneiden gewährleistet Effizienz und Komfort bei Skitouren.", false, "Fell zuschneiden pro Paar", 25 },
                    { 6, "bietet tiefe Wachsimprägnierung für herausragendes Gleiterlebnis. Verfügbar als eigenständige Dienstleistung zur optimalen Vorbereitung der Skier für maximale Performance bei jedem Abfahrtslauf.", false, "Heisswachsen", 18 }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Offen" },
                    { 2, false, "InArbeit" },
                    { 3, false, "Abgeschlossen" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Created", "Email", "IsDeleted", "Name", "Note", "Phone", "PriorityId", "ServiceId", "StateId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 2, 8, 55, 18, 807, DateTimeKind.Local).AddTicks(2452), "alice.johnson@example.com", false, "Alice Johnson", null, "+15703464001", 1, 3, 2, null },
                    { 2, new DateTime(2023, 12, 2, 8, 55, 18, 807, DateTimeKind.Local).AddTicks(2456), "bob.smith@example.com", false, "Bob Smith", null, "+15703464002", 2, 1, 3, null },
                    { 3, new DateTime(2023, 12, 2, 8, 55, 18, 807, DateTimeKind.Local).AddTicks(2459), "carol.white@example.com", false, "Carol White", null, "+15703464003", 3, 5, 1, null },
                    { 4, new DateTime(2023, 12, 2, 8, 55, 18, 807, DateTimeKind.Local).AddTicks(2463), "david.green@example.com", false, "David Green", null, "+15703464004", 1, 2, 2, null },
                    { 5, new DateTime(2023, 12, 2, 8, 55, 18, 807, DateTimeKind.Local).AddTicks(2466), "evelyn.harris@example.com", false, "Evelyn Harris", "Check for additional details", "+15703464005", 2, 4, 3, null },
                    { 6, new DateTime(2023, 12, 2, 8, 55, 18, 807, DateTimeKind.Local).AddTicks(2470), "frank.miller@example.com", false, "Frank Miller", null, "+15703464006", 3, 6, 1, null },
                    { 7, new DateTime(2023, 12, 2, 8, 55, 18, 807, DateTimeKind.Local).AddTicks(2473), "grace.lee@example.com", false, "Grace Lee", null, "+15703464007", 1, 1, 2, null },
                    { 8, new DateTime(2023, 12, 2, 8, 55, 18, 807, DateTimeKind.Local).AddTicks(2476), "henry.wilson@example.com", false, "Henry Wilson", null, "+15703464008", 2, 3, 3, null },
                    { 9, new DateTime(2023, 12, 2, 8, 55, 18, 807, DateTimeKind.Local).AddTicks(2532), "irene.taylor@example.com", false, "Irene Taylor", "Requires immediate attention", "+15703464009", 3, 5, 1, null },
                    { 10, new DateTime(2023, 12, 2, 8, 55, 18, 807, DateTimeKind.Local).AddTicks(2536), "jason.brown@example.com", false, "Jason Brown", null, "+15703464010", 1, 2, 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PriorityId",
                table: "Orders",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServiceId",
                table: "Orders",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StateId",
                table: "Orders",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
