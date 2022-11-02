using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HormoneTracker.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Data",
                columns: table => new
                {
                    DataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: true),
                    NormCoefficient = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Data", x => x.DataId);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MidName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.DoctorId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MidName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patient_Doctor",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "DoctorId");
                });

            migrationBuilder.CreateTable(
                name: "ProductData",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductData", x => new { x.ProductId, x.DataId });
                    table.ForeignKey(
                        name: "FK_ProductData_Data",
                        column: x => x.DataId,
                        principalTable: "Data",
                        principalColumn: "DataId");
                    table.ForeignKey(
                        name: "FK_ProductData_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "Analysis",
                columns: table => new
                {
                    AnalysisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analysis", x => x.AnalysisId);
                    table.ForeignKey(
                        name: "FK_Analysis_Patient",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_Analysis_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountLast = table.Column<int>(type: "int", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastDoseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.MedicineId);
                    table.ForeignKey(
                        name: "FK_Medicine_Patient",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "Tip",
                columns: table => new
                {
                    TipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tip", x => x.TipId);
                    table.ForeignKey(
                        name: "FK_Tip_Patient",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "AnalysisData",
                columns: table => new
                {
                    AnalysisId = table.Column<int>(type: "int", nullable: false),
                    DataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisData", x => new { x.AnalysisId, x.DataId });
                    table.ForeignKey(
                        name: "FK_AnalysisData_Analysis",
                        column: x => x.AnalysisId,
                        principalTable: "Analysis",
                        principalColumn: "AnalysisId");
                    table.ForeignKey(
                        name: "FK_AnalysisData_Data",
                        column: x => x.DataId,
                        principalTable: "Data",
                        principalColumn: "DataId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analysis_PatientId",
                table: "Analysis",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Analysis_StatusId",
                table: "Analysis",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisData_DataId",
                table: "AnalysisData",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_PatientId",
                table: "Medicine",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_DoctorId",
                table: "Patient",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductData_DataId",
                table: "ProductData",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_Tip_PatientId",
                table: "Tip",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "AnalysisData");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "ProductData");

            migrationBuilder.DropTable(
                name: "Tip");

            migrationBuilder.DropTable(
                name: "Analysis");

            migrationBuilder.DropTable(
                name: "Data");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Doctor");
        }
    }
}
