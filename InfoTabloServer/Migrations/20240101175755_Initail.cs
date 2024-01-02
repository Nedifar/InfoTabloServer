using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfoTabloServer.Migrations
{
    /// <inheritdoc />
    public partial class Initail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    idAnnouncement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dateClosed = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Priority = table.Column<string>(type: "text", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.idAnnouncement);
                });

            migrationBuilder.CreateTable(
                name: "DayPartHeaders",
                columns: table => new
                {
                    DayPartHeaderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(type: "text", nullable: true),
                    beginTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    endTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayPartHeaders", x => x.DayPartHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "DayWeeks",
                columns: table => new
                {
                    idDayWeek = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWeeks", x => x.idDayWeek);
                });

            migrationBuilder.CreateTable(
                name: "MonthYear",
                columns: table => new
                {
                    idMonthYear = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthYear", x => x.idMonthYear);
                });

            migrationBuilder.CreateTable(
                name: "SheduleAdditionalLessons",
                columns: table => new
                {
                    idSheduleAdditionalLesson = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    durationLesson = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheduleAdditionalLessons", x => x.idSheduleAdditionalLesson);
                });

            migrationBuilder.CreateTable(
                name: "SpecialBackgroundPhotos",
                columns: table => new
                {
                    idSpecialBackgroundPhoto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    targetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    fileName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialBackgroundPhotos", x => x.idSpecialBackgroundPhoto);
                });

            migrationBuilder.CreateTable(
                name: "SpecialDayWeekNames",
                columns: table => new
                {
                    idSpecialDayWeekName = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dayWeek = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDayWeekNames", x => x.idSpecialDayWeekName);
                });

            migrationBuilder.CreateTable(
                name: "SupervisorShedules",
                columns: table => new
                {
                    idSupervisorShedule = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameSupervisor = table.Column<string>(type: "text", nullable: true),
                    position = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorShedules", x => x.idSupervisorShedule);
                });

            migrationBuilder.CreateTable(
                name: "TimeShedules",
                columns: table => new
                {
                    idTimeShedule = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeShedules", x => x.idTimeShedule);
                });

            migrationBuilder.CreateTable(
                name: "TypeIntervals",
                columns: table => new
                {
                    idInterval = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeIntervals", x => x.idInterval);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PasHash = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "WeekNames",
                columns: table => new
                {
                    idWeekName = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Begin = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekNames", x => x.idWeekName);
                });

            migrationBuilder.CreateTable(
                name: "DayWeekSheduleAdditionalLesson",
                columns: table => new
                {
                    DayWeeksidDayWeek = table.Column<int>(type: "integer", nullable: false),
                    SheduleAdditionalLessonsidSheduleAdditionalLesson = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWeekSheduleAdditionalLesson", x => new { x.DayWeeksidDayWeek, x.SheduleAdditionalLessonsidSheduleAdditionalLesson });
                    table.ForeignKey(
                        name: "FK_DayWeekSheduleAdditionalLesson_DayWeeks_DayWeeksidDayWeek",
                        column: x => x.DayWeeksidDayWeek,
                        principalTable: "DayWeeks",
                        principalColumn: "idDayWeek",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayWeekSheduleAdditionalLesson_SheduleAdditionalLessons_She~",
                        column: x => x.SheduleAdditionalLessonsidSheduleAdditionalLesson,
                        principalTable: "SheduleAdditionalLessons",
                        principalColumn: "idSheduleAdditionalLesson",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    idTime = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    beginTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    idSheduleAdditionalLesson = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.idTime);
                    table.ForeignKey(
                        name: "FK_Times_SheduleAdditionalLessons_idSheduleAdditionalLesson",
                        column: x => x.idSheduleAdditionalLesson,
                        principalTable: "SheduleAdditionalLessons",
                        principalColumn: "idSheduleAdditionalLesson",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatesSupervisiors",
                columns: table => new
                {
                    idDatesSupervisior = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    idSupervisorShedule = table.Column<int>(type: "integer", nullable: false),
                    d1 = table.Column<bool>(type: "boolean", nullable: false),
                    d2 = table.Column<bool>(type: "boolean", nullable: false),
                    d3 = table.Column<bool>(type: "boolean", nullable: false),
                    d4 = table.Column<bool>(type: "boolean", nullable: false),
                    d5 = table.Column<bool>(type: "boolean", nullable: false),
                    d6 = table.Column<bool>(type: "boolean", nullable: false),
                    d7 = table.Column<bool>(type: "boolean", nullable: false),
                    d8 = table.Column<bool>(type: "boolean", nullable: false),
                    d9 = table.Column<bool>(type: "boolean", nullable: false),
                    d10 = table.Column<bool>(type: "boolean", nullable: false),
                    d11 = table.Column<bool>(type: "boolean", nullable: false),
                    d12 = table.Column<bool>(type: "boolean", nullable: false),
                    d13 = table.Column<bool>(type: "boolean", nullable: false),
                    d14 = table.Column<bool>(type: "boolean", nullable: false),
                    d15 = table.Column<bool>(type: "boolean", nullable: false),
                    d16 = table.Column<bool>(type: "boolean", nullable: false),
                    d17 = table.Column<bool>(type: "boolean", nullable: false),
                    d18 = table.Column<bool>(type: "boolean", nullable: false),
                    d19 = table.Column<bool>(type: "boolean", nullable: false),
                    d20 = table.Column<bool>(type: "boolean", nullable: false),
                    d21 = table.Column<bool>(type: "boolean", nullable: false),
                    d22 = table.Column<bool>(type: "boolean", nullable: false),
                    d23 = table.Column<bool>(type: "boolean", nullable: false),
                    d24 = table.Column<bool>(type: "boolean", nullable: false),
                    d25 = table.Column<bool>(type: "boolean", nullable: false),
                    d26 = table.Column<bool>(type: "boolean", nullable: false),
                    d27 = table.Column<bool>(type: "boolean", nullable: false),
                    d28 = table.Column<bool>(type: "boolean", nullable: false),
                    d29 = table.Column<bool>(type: "boolean", nullable: false),
                    d30 = table.Column<bool>(type: "boolean", nullable: false),
                    d31 = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatesSupervisiors", x => x.idDatesSupervisior);
                    table.ForeignKey(
                        name: "FK_DatesSupervisiors_SupervisorShedules_idSupervisorShedule",
                        column: x => x.idSupervisorShedule,
                        principalTable: "SupervisorShedules",
                        principalColumn: "idSupervisorShedule",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthYearSupervisorShedule",
                columns: table => new
                {
                    MonthYearsidMonthYear = table.Column<int>(type: "integer", nullable: false),
                    SupervisorShedulesidSupervisorShedule = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthYearSupervisorShedule", x => new { x.MonthYearsidMonthYear, x.SupervisorShedulesidSupervisorShedule });
                    table.ForeignKey(
                        name: "FK_MonthYearSupervisorShedule_MonthYear_MonthYearsidMonthYear",
                        column: x => x.MonthYearsidMonthYear,
                        principalTable: "MonthYear",
                        principalColumn: "idMonthYear",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonthYearSupervisorShedule_SupervisorShedules_SupervisorShe~",
                        column: x => x.SupervisorShedulesidSupervisorShedule,
                        principalTable: "SupervisorShedules",
                        principalColumn: "idSupervisorShedule",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paras",
                columns: table => new
                {
                    idPara = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    begin = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    numberInList = table.Column<int>(type: "integer", nullable: false),
                    numberInterval = table.Column<int>(type: "integer", nullable: false),
                    idTypeInterval = table.Column<int>(type: "integer", nullable: false),
                    idTimeShedule = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paras", x => x.idPara);
                    table.ForeignKey(
                        name: "FK_Paras_TimeShedules_idTimeShedule",
                        column: x => x.idTimeShedule,
                        principalTable: "TimeShedules",
                        principalColumn: "idTimeShedule",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paras_TypeIntervals_idTypeInterval",
                        column: x => x.idTypeInterval,
                        principalTable: "TypeIntervals",
                        principalColumn: "idInterval",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    idLesson = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    groupName = table.Column<string>(type: "text", nullable: true),
                    teacherName = table.Column<string>(type: "text", nullable: true),
                    cabinet = table.Column<string>(type: "text", nullable: true),
                    idDayWeek = table.Column<int>(type: "integer", nullable: false),
                    idTime = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.idLesson);
                    table.ForeignKey(
                        name: "FK_Lessons_DayWeeks_idDayWeek",
                        column: x => x.idDayWeek,
                        principalTable: "DayWeeks",
                        principalColumn: "idDayWeek",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Times_idTime",
                        column: x => x.idTime,
                        principalTable: "Times",
                        principalColumn: "idTime",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatesSupervisiors_idSupervisorShedule",
                table: "DatesSupervisiors",
                column: "idSupervisorShedule");

            migrationBuilder.CreateIndex(
                name: "IX_DayWeekSheduleAdditionalLesson_SheduleAdditionalLessonsidSh~",
                table: "DayWeekSheduleAdditionalLesson",
                column: "SheduleAdditionalLessonsidSheduleAdditionalLesson");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_idDayWeek",
                table: "Lessons",
                column: "idDayWeek");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_idTime",
                table: "Lessons",
                column: "idTime");

            migrationBuilder.CreateIndex(
                name: "IX_MonthYearSupervisorShedule_SupervisorShedulesidSupervisorSh~",
                table: "MonthYearSupervisorShedule",
                column: "SupervisorShedulesidSupervisorShedule");

            migrationBuilder.CreateIndex(
                name: "IX_Paras_idTimeShedule",
                table: "Paras",
                column: "idTimeShedule");

            migrationBuilder.CreateIndex(
                name: "IX_Paras_idTypeInterval",
                table: "Paras",
                column: "idTypeInterval");

            migrationBuilder.CreateIndex(
                name: "IX_Times_idSheduleAdditionalLesson",
                table: "Times",
                column: "idSheduleAdditionalLesson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "DatesSupervisiors");

            migrationBuilder.DropTable(
                name: "DayPartHeaders");

            migrationBuilder.DropTable(
                name: "DayWeekSheduleAdditionalLesson");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "MonthYearSupervisorShedule");

            migrationBuilder.DropTable(
                name: "Paras");

            migrationBuilder.DropTable(
                name: "SpecialBackgroundPhotos");

            migrationBuilder.DropTable(
                name: "SpecialDayWeekNames");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WeekNames");

            migrationBuilder.DropTable(
                name: "DayWeeks");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "MonthYear");

            migrationBuilder.DropTable(
                name: "SupervisorShedules");

            migrationBuilder.DropTable(
                name: "TimeShedules");

            migrationBuilder.DropTable(
                name: "TypeIntervals");

            migrationBuilder.DropTable(
                name: "SheduleAdditionalLessons");
        }
    }
}
