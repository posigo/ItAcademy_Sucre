using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sucre_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableAndInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cexs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Management = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    CexName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Device = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cexs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enegies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnergyName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enegies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParameterTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Mnemo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    UnitMeas = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EnergyId = table.Column<int>(type: "int", nullable: false),
                    CexId = table.Column<int>(type: "int", nullable: false),
                    ServiceStaff = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Points_Cexs_CexId",
                        column: x => x.CexId,
                        principalTable: "Cexs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Points_Enegies_EnergyId",
                        column: x => x.EnergyId,
                        principalTable: "Enegies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_GroupUsers_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Canals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ParameterTypeId = table.Column<int>(type: "int", nullable: false),
                    Reader = table.Column<bool>(type: "bit", nullable: false),
                    SourceType = table.Column<int>(type: "int", nullable: false),
                    AsPazEin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canals_ParameterTypes_ParameterTypeId",
                        column: x => x.ParameterTypeId,
                        principalTable: "ParameterTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUserReport",
                columns: table => new
                {
                    GroupUsersId = table.Column<int>(type: "int", nullable: false),
                    ReportsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUserReport", x => new { x.GroupUsersId, x.ReportsId });
                    table.ForeignKey(
                        name: "FK_GroupUserReport_GroupUsers_GroupUsersId",
                        column: x => x.GroupUsersId,
                        principalTable: "GroupUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUserReport_Reports_ReportsId",
                        column: x => x.ReportsId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AsPazs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A_High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    W_High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    W_Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A_Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A_HighEin = table.Column<bool>(type: "bit", nullable: false),
                    W_HighEin = table.Column<bool>(type: "bit", nullable: false),
                    W_LowEin = table.Column<bool>(type: "bit", nullable: false),
                    A_LowEin = table.Column<bool>(type: "bit", nullable: false),
                    A_HighType = table.Column<bool>(type: "bit", nullable: false),
                    W_HighType = table.Column<bool>(type: "bit", nullable: false),
                    W_LowType = table.Column<bool>(type: "bit", nullable: false),
                    A_LowType = table.Column<bool>(type: "bit", nullable: false),
                    CanalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsPazs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsPazs_Canals_CanalId",
                        column: x => x.CanalId,
                        principalTable: "Canals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CanalPoint",
                columns: table => new
                {
                    CanalsId = table.Column<int>(type: "int", nullable: false),
                    PointsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanalPoint", x => new { x.CanalsId, x.PointsId });
                    table.ForeignKey(
                        name: "FK_CanalPoint_Canals_CanalsId",
                        column: x => x.CanalsId,
                        principalTable: "Canals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CanalPoint_Points_PointsId",
                        column: x => x.PointsId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportDetails",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    PointId = table.Column<int>(type: "int", nullable: false),
                    CanalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ReportDetails_Canals_CanalId",
                        column: x => x.CanalId,
                        principalTable: "Canals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportDetails_Points_PointId",
                        column: x => x.PointId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportDetails_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValuesDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Changed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuesDay", x => new { x.Id, x.Date });
                    table.ForeignKey(
                        name: "FK_ValuesDay_Canals_Id",
                        column: x => x.Id,
                        principalTable: "Canals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValuesHour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Changed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuesHour", x => new { x.Id, x.Date, x.Hour });
                    table.ForeignKey(
                        name: "FK_ValuesHour_Canals_Id",
                        column: x => x.Id,
                        principalTable: "Canals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValuesMounth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Changed = table.Column<bool>(type: "bit", nullable: false),
                    PlanFact = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuesMounth", x => new { x.Id, x.Date });
                    table.ForeignKey(
                        name: "FK_ValuesMounth_Canals_Id",
                        column: x => x.Id,
                        principalTable: "Canals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsPazs_CanalId",
                table: "AsPazs",
                column: "CanalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CanalPoint_PointsId",
                table: "CanalPoint",
                column: "PointsId");

            migrationBuilder.CreateIndex(
                name: "IX_Canals_ParameterTypeId",
                table: "Canals",
                column: "ParameterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserReport_ReportsId",
                table: "GroupUserReport",
                column: "ReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_CexId",
                table: "Points",
                column: "CexId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_EnergyId",
                table: "Points",
                column: "EnergyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDetails_CanalId",
                table: "ReportDetails",
                column: "CanalId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDetails_PointId",
                table: "ReportDetails",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDetails_ReportId",
                table: "ReportDetails",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                table: "Users",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsPazs");

            migrationBuilder.DropTable(
                name: "CanalPoint");

            migrationBuilder.DropTable(
                name: "GroupUserReport");

            migrationBuilder.DropTable(
                name: "ReportDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ValuesDay");

            migrationBuilder.DropTable(
                name: "ValuesHour");

            migrationBuilder.DropTable(
                name: "ValuesMounth");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "GroupUsers");

            migrationBuilder.DropTable(
                name: "Canals");

            migrationBuilder.DropTable(
                name: "Cexs");

            migrationBuilder.DropTable(
                name: "Enegies");

            migrationBuilder.DropTable(
                name: "ParameterTypes");
        }
    }
}
