using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Clean.Persistence.Migrations
{
    public partial class MIG02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "prc");

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Process",
                schema: "prc",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    ScreenID = table.Column<int>(nullable: true),
                    Sorter = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.ID);
                    table.ForeignKey(
                        name: "_Process__FK",
                        column: x => x.ScreenID,
                        principalSchema: "Look",
                        principalTable: "Screen",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    ObjectSchema = table.Column<string>(nullable: true),
                    ObjectName = table.Column<string>(nullable: true),
                    RecordId = table.Column<long>(nullable: false),
                    Root = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EncryptionKey = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    ScreenId = table.Column<int>(nullable: true),
                    LastDownloadDate = table.Column<DateTime>(nullable: true),
                    DocumentNumber = table.Column<string>(nullable: true),
                    DocumentSource = table.Column<string>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: true),
                    DocumentTypeId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScreenId = table.Column<int>(nullable: true),
                    DocumentTypeId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenDocument_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScreenDocument_Screen_ScreenId",
                        column: x => x.ScreenId,
                        principalSchema: "Look",
                        principalTable: "Screen",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessConnection",
                schema: "prc",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    ProcessID = table.Column<int>(nullable: false),
                    ConnectedTo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessConnection", x => x.ID);
                    table.ForeignKey(
                        name: "_ProcessConnection__FK_1",
                        column: x => x.ConnectedTo,
                        principalSchema: "prc",
                        principalTable: "Process",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "_ProcessConnection__FK",
                        column: x => x.ProcessID,
                        principalSchema: "prc",
                        principalTable: "Process",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessTracking",
                schema: "prc",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    RecordID = table.Column<long>(nullable: false),
                    ProcessID = table.Column<int>(nullable: false),
                    ReferedProcessID = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 1000, nullable: true),
                    ModuleID = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    ToUserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessTracking", x => x.ID);
                    table.ForeignKey(
                        name: "_ProcessTracking__FK_2",
                        column: x => x.ModuleID,
                        principalSchema: "Look",
                        principalTable: "Module",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "_ProcessTracking__FK",
                        column: x => x.ProcessID,
                        principalSchema: "prc",
                        principalTable: "Process",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "_ProcessTracking__FK_1",
                        column: x => x.ReferedProcessID,
                        principalSchema: "prc",
                        principalTable: "Process",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeId",
                table: "Documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenDocument_DocumentTypeId",
                table: "ScreenDocument",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenDocument_ScreenId",
                table: "ScreenDocument",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_Process_ScreenID",
                schema: "prc",
                table: "Process",
                column: "ScreenID");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessConnection_ConnectedTo",
                schema: "prc",
                table: "ProcessConnection",
                column: "ConnectedTo");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessConnection_ProcessID",
                schema: "prc",
                table: "ProcessConnection",
                column: "ProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessTracking_ModuleID",
                schema: "prc",
                table: "ProcessTracking",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessTracking_ProcessID",
                schema: "prc",
                table: "ProcessTracking",
                column: "ProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessTracking_ReferedProcessID",
                schema: "prc",
                table: "ProcessTracking",
                column: "ReferedProcessID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "ScreenDocument");

            migrationBuilder.DropTable(
                name: "ProcessConnection",
                schema: "prc");

            migrationBuilder.DropTable(
                name: "ProcessTracking",
                schema: "prc");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "Process",
                schema: "prc");
        }
    }
}
