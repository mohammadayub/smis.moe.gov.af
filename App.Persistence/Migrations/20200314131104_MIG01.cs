using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Persistence.Migrations
{
    public partial class MIG01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "au");

            migrationBuilder.EnsureSchema(
                name: "doc");

            migrationBuilder.EnsureSchema(
                name: "Look");

            migrationBuilder.EnsureSchema(
                name: "prc");

            migrationBuilder.CreateTable(
                name: "OperationType",
                schema: "au",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    OperationTypeName = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                schema: "doc",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    Category = table.Column<string>(type: "character varying", nullable: true),
                    Description = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                schema: "Look",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Sorter = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SystemStatus",
                schema: "Look",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    StatusType = table.Column<string>(type: "character varying", nullable: false),
                    TypeID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Sorter = table.Column<string>(maxLength: 100, nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Audit",
                schema: "au",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    DbContextObject = table.Column<string>(maxLength: 100, nullable: true),
                    DbObjectName = table.Column<string>(maxLength: 100, nullable: true),
                    RecordID = table.Column<string>(maxLength: 200, nullable: true),
                    OperationTypeID = table.Column<int>(nullable: true),
                    ValueBefore = table.Column<string>(type: "character varying", nullable: true),
                    ValueAfter = table.Column<string>(type: "character varying", nullable: true),
                    OperationDate = table.Column<DateTime>(nullable: true),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.ID);
                    table.ForeignKey(
                        name: "audit_fk",
                        column: x => x.OperationTypeID,
                        principalSchema: "au",
                        principalTable: "OperationType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "doc",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    FileName = table.Column<string>(maxLength: 200, nullable: false),
                    ContentType = table.Column<string>(maxLength: 200, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ObjectSchema = table.Column<string>(maxLength: 100, nullable: true),
                    ObjectName = table.Column<string>(maxLength: 100, nullable: true),
                    RecordID = table.Column<long>(nullable: false),
                    Root = table.Column<string>(maxLength: 200, nullable: true),
                    Path = table.Column<string>(maxLength: 1000, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    EncryptionKey = table.Column<string>(maxLength: 500, nullable: true),
                    StatusID = table.Column<int>(nullable: true),
                    ScreenID = table.Column<int>(nullable: true),
                    LastDownloadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DocumentNumber = table.Column<string>(maxLength: 100, nullable: true),
                    DocumentSource = table.Column<string>(maxLength: 100, nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DocumentTypeID = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.ID);
                    table.ForeignKey(
                        name: "_Documents__FK",
                        column: x => x.DocumentTypeID,
                        principalSchema: "doc",
                        principalTable: "DocumentType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Screen",
                schema: "Look",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    DirectoryPath = table.Column<string>(maxLength: 500, nullable: false),
                    Icon = table.Column<string>(maxLength: 200, nullable: false),
                    Sorter = table.Column<int>(nullable: false),
                    ParentID = table.Column<int>(nullable: true),
                    ModuleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screen", x => x.ID);
                    table.ForeignKey(
                        name: "screen_fk",
                        column: x => x.ModuleID,
                        principalSchema: "Look",
                        principalTable: "Module",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "screen_parent_fk",
                        column: x => x.ParentID,
                        principalSchema: "Look",
                        principalTable: "Screen",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleScreen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ScreenId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleScreen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleScreen_Screen_ScreenId",
                        column: x => x.ScreenId,
                        principalSchema: "Look",
                        principalTable: "Screen",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenDocument",
                schema: "doc",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    ScreenID = table.Column<int>(nullable: true),
                    DocumentTypeID = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenDocument", x => x.ID);
                    table.ForeignKey(
                        name: "_ScreenDocument__FK",
                        column: x => x.DocumentTypeID,
                        principalSchema: "doc",
                        principalTable: "DocumentType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "_ScreenDocument__FK_1",
                        column: x => x.ScreenID,
                        principalSchema: "Look",
                        principalTable: "Screen",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    UserID = table.Column<string>(type: "character varying", nullable: false),
                    ToUserID = table.Column<string>(type: "character varying", nullable: true)
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
                name: "IX_RoleScreen_ScreenId",
                table: "RoleScreen",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_Audit_OperationTypeID",
                schema: "au",
                table: "Audit",
                column: "OperationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeID",
                schema: "doc",
                table: "Documents",
                column: "DocumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenDocument_DocumentTypeID",
                schema: "doc",
                table: "ScreenDocument",
                column: "DocumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenDocument_ScreenID",
                schema: "doc",
                table: "ScreenDocument",
                column: "ScreenID");

            migrationBuilder.CreateIndex(
                name: "IX_Screen_ModuleID",
                schema: "Look",
                table: "Screen",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "IX_Screen_ParentID",
                schema: "Look",
                table: "Screen",
                column: "ParentID");

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
                name: "RoleScreen");

            migrationBuilder.DropTable(
                name: "Audit",
                schema: "au");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "ScreenDocument",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "SystemStatus",
                schema: "Look");

            migrationBuilder.DropTable(
                name: "ProcessConnection",
                schema: "prc");

            migrationBuilder.DropTable(
                name: "ProcessTracking",
                schema: "prc");

            migrationBuilder.DropTable(
                name: "OperationType",
                schema: "au");

            migrationBuilder.DropTable(
                name: "DocumentType",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "Process",
                schema: "prc");

            migrationBuilder.DropTable(
                name: "Screen",
                schema: "Look");

            migrationBuilder.DropTable(
                name: "Module",
                schema: "Look");
        }
    }
}
