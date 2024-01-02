﻿// <auto-generated />
using System;
using InfoTabloServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfoTabloServer.Migrations
{
    [DbContext(typeof(context))]
    [Migration("20240101175755_Initail")]
    partial class Initail
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DayWeekSheduleAdditionalLesson", b =>
                {
                    b.Property<int>("DayWeeksidDayWeek")
                        .HasColumnType("integer");

                    b.Property<int>("SheduleAdditionalLessonsidSheduleAdditionalLesson")
                        .HasColumnType("integer");

                    b.HasKey("DayWeeksidDayWeek", "SheduleAdditionalLessonsidSheduleAdditionalLesson");

                    b.HasIndex("SheduleAdditionalLessonsidSheduleAdditionalLesson");

                    b.ToTable("DayWeekSheduleAdditionalLesson");
                });

            modelBuilder.Entity("InfoTabloServer.Models.AdditionalLessonsModels.DayWeek", b =>
                {
                    b.Property<int>("idDayWeek")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idDayWeek"));

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.HasKey("idDayWeek");

                    b.ToTable("DayWeeks");
                });

            modelBuilder.Entity("InfoTabloServer.Models.AdditionalLessonsModels.Lesson", b =>
                {
                    b.Property<int>("idLesson")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idLesson"));

                    b.Property<string>("cabinet")
                        .HasColumnType("text");

                    b.Property<string>("groupName")
                        .HasColumnType("text");

                    b.Property<int>("idDayWeek")
                        .HasColumnType("integer");

                    b.Property<int>("idTime")
                        .HasColumnType("integer");

                    b.Property<string>("teacherName")
                        .HasColumnType("text");

                    b.HasKey("idLesson");

                    b.HasIndex("idDayWeek");

                    b.HasIndex("idTime");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("InfoTabloServer.Models.AdditionalLessonsModels.SheduleAdditionalLesson", b =>
                {
                    b.Property<int>("idSheduleAdditionalLesson")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idSheduleAdditionalLesson"));

                    b.Property<int>("durationLesson")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.HasKey("idSheduleAdditionalLesson");

                    b.ToTable("SheduleAdditionalLessons");
                });

            modelBuilder.Entity("InfoTabloServer.Models.AdditionalLessonsModels.Time", b =>
                {
                    b.Property<int>("idTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idTime"));

                    b.Property<DateTime>("beginTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("idSheduleAdditionalLesson")
                        .HasColumnType("integer");

                    b.HasKey("idTime");

                    b.HasIndex("idSheduleAdditionalLesson");

                    b.ToTable("Times");
                });

            modelBuilder.Entity("InfoTabloServer.Models.Announcement", b =>
                {
                    b.Property<int>("idAnnouncement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idAnnouncement"));

                    b.Property<string>("Header")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Priority")
                        .HasColumnType("text");

                    b.Property<DateTime>("dateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("dateClosed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("status")
                        .HasColumnType("text");

                    b.HasKey("idAnnouncement");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("InfoTabloServer.Models.DatesSupervisior", b =>
                {
                    b.Property<int>("idDatesSupervisior")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idDatesSupervisior"));

                    b.Property<bool>("d1")
                        .HasColumnType("boolean");

                    b.Property<bool>("d10")
                        .HasColumnType("boolean");

                    b.Property<bool>("d11")
                        .HasColumnType("boolean");

                    b.Property<bool>("d12")
                        .HasColumnType("boolean");

                    b.Property<bool>("d13")
                        .HasColumnType("boolean");

                    b.Property<bool>("d14")
                        .HasColumnType("boolean");

                    b.Property<bool>("d15")
                        .HasColumnType("boolean");

                    b.Property<bool>("d16")
                        .HasColumnType("boolean");

                    b.Property<bool>("d17")
                        .HasColumnType("boolean");

                    b.Property<bool>("d18")
                        .HasColumnType("boolean");

                    b.Property<bool>("d19")
                        .HasColumnType("boolean");

                    b.Property<bool>("d2")
                        .HasColumnType("boolean");

                    b.Property<bool>("d20")
                        .HasColumnType("boolean");

                    b.Property<bool>("d21")
                        .HasColumnType("boolean");

                    b.Property<bool>("d22")
                        .HasColumnType("boolean");

                    b.Property<bool>("d23")
                        .HasColumnType("boolean");

                    b.Property<bool>("d24")
                        .HasColumnType("boolean");

                    b.Property<bool>("d25")
                        .HasColumnType("boolean");

                    b.Property<bool>("d26")
                        .HasColumnType("boolean");

                    b.Property<bool>("d27")
                        .HasColumnType("boolean");

                    b.Property<bool>("d28")
                        .HasColumnType("boolean");

                    b.Property<bool>("d29")
                        .HasColumnType("boolean");

                    b.Property<bool>("d3")
                        .HasColumnType("boolean");

                    b.Property<bool>("d30")
                        .HasColumnType("boolean");

                    b.Property<bool>("d31")
                        .HasColumnType("boolean");

                    b.Property<bool>("d4")
                        .HasColumnType("boolean");

                    b.Property<bool>("d5")
                        .HasColumnType("boolean");

                    b.Property<bool>("d6")
                        .HasColumnType("boolean");

                    b.Property<bool>("d7")
                        .HasColumnType("boolean");

                    b.Property<bool>("d8")
                        .HasColumnType("boolean");

                    b.Property<bool>("d9")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("idSupervisorShedule")
                        .HasColumnType("integer");

                    b.HasKey("idDatesSupervisior");

                    b.HasIndex("idSupervisorShedule");

                    b.ToTable("DatesSupervisiors");
                });

            modelBuilder.Entity("InfoTabloServer.Models.DayPartHeader", b =>
                {
                    b.Property<int>("DayPartHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DayPartHeaderId"));

                    b.Property<string>("Header")
                        .HasColumnType("text");

                    b.Property<DateTime>("beginTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("endTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("DayPartHeaderId");

                    b.ToTable("DayPartHeaders");
                });

            modelBuilder.Entity("InfoTabloServer.Models.MonthYear", b =>
                {
                    b.Property<int>("idMonthYear")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idMonthYear"));

                    b.Property<DateTime>("date")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("idMonthYear");

                    b.ToTable("MonthYear");
                });

            modelBuilder.Entity("InfoTabloServer.Models.Para", b =>
                {
                    b.Property<int>("idPara")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idPara"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("begin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("end")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("idTimeShedule")
                        .HasColumnType("integer");

                    b.Property<int>("idTypeInterval")
                        .HasColumnType("integer");

                    b.Property<int>("numberInList")
                        .HasColumnType("integer");

                    b.Property<int>("numberInterval")
                        .HasColumnType("integer");

                    b.HasKey("idPara");

                    b.HasIndex("idTimeShedule");

                    b.HasIndex("idTypeInterval");

                    b.ToTable("Paras");
                });

            modelBuilder.Entity("InfoTabloServer.Models.SpecialBackgroundPhoto", b =>
                {
                    b.Property<int>("idSpecialBackgroundPhoto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idSpecialBackgroundPhoto"));

                    b.Property<string>("fileName")
                        .HasColumnType("text");

                    b.Property<DateTime?>("targetDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("idSpecialBackgroundPhoto");

                    b.ToTable("SpecialBackgroundPhotos");
                });

            modelBuilder.Entity("InfoTabloServer.Models.SpecialDayWeekName", b =>
                {
                    b.Property<int>("idSpecialDayWeekName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idSpecialDayWeekName"));

                    b.Property<int>("dayWeek")
                        .HasColumnType("integer");

                    b.HasKey("idSpecialDayWeekName");

                    b.ToTable("SpecialDayWeekNames");
                });

            modelBuilder.Entity("InfoTabloServer.Models.SupervisorShedule", b =>
                {
                    b.Property<int>("idSupervisorShedule")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idSupervisorShedule"));

                    b.Property<string>("NameSupervisor")
                        .HasColumnType("text");

                    b.Property<string>("position")
                        .HasColumnType("text");

                    b.HasKey("idSupervisorShedule");

                    b.ToTable("SupervisorShedules");
                });

            modelBuilder.Entity("InfoTabloServer.Models.TimeShedule", b =>
                {
                    b.Property<int>("idTimeShedule")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idTimeShedule"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("idTimeShedule");

                    b.ToTable("TimeShedules");
                });

            modelBuilder.Entity("InfoTabloServer.Models.TypeInterval", b =>
                {
                    b.Property<int>("idInterval")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idInterval"));

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.HasKey("idInterval");

                    b.ToTable("TypeIntervals");
                });

            modelBuilder.Entity("InfoTabloServer.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdUser"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PasHash")
                        .HasColumnType("text");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InfoTabloServer.Models.WeekName", b =>
                {
                    b.Property<int>("idWeekName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idWeekName"));

                    b.Property<DateTime>("Begin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("idWeekName");

                    b.ToTable("WeekNames");
                });

            modelBuilder.Entity("MonthYearSupervisorShedule", b =>
                {
                    b.Property<int>("MonthYearsidMonthYear")
                        .HasColumnType("integer");

                    b.Property<int>("SupervisorShedulesidSupervisorShedule")
                        .HasColumnType("integer");

                    b.HasKey("MonthYearsidMonthYear", "SupervisorShedulesidSupervisorShedule");

                    b.HasIndex("SupervisorShedulesidSupervisorShedule");

                    b.ToTable("MonthYearSupervisorShedule");
                });

            modelBuilder.Entity("DayWeekSheduleAdditionalLesson", b =>
                {
                    b.HasOne("InfoTabloServer.Models.AdditionalLessonsModels.DayWeek", null)
                        .WithMany()
                        .HasForeignKey("DayWeeksidDayWeek")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfoTabloServer.Models.AdditionalLessonsModels.SheduleAdditionalLesson", null)
                        .WithMany()
                        .HasForeignKey("SheduleAdditionalLessonsidSheduleAdditionalLesson")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InfoTabloServer.Models.AdditionalLessonsModels.Lesson", b =>
                {
                    b.HasOne("InfoTabloServer.Models.AdditionalLessonsModels.DayWeek", "DayWeek")
                        .WithMany("Lessons")
                        .HasForeignKey("idDayWeek")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfoTabloServer.Models.AdditionalLessonsModels.Time", "Time")
                        .WithMany("Lessons")
                        .HasForeignKey("idTime")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DayWeek");

                    b.Navigation("Time");
                });

            modelBuilder.Entity("InfoTabloServer.Models.AdditionalLessonsModels.Time", b =>
                {
                    b.HasOne("InfoTabloServer.Models.AdditionalLessonsModels.SheduleAdditionalLesson", "SheduleAdditionalLesson")
                        .WithMany("Times")
                        .HasForeignKey("idSheduleAdditionalLesson")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SheduleAdditionalLesson");
                });

            modelBuilder.Entity("InfoTabloServer.Models.DatesSupervisior", b =>
                {
                    b.HasOne("InfoTabloServer.Models.SupervisorShedule", "SupervisorShedule")
                        .WithMany("DatesSupervisiors")
                        .HasForeignKey("idSupervisorShedule")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SupervisorShedule");
                });

            modelBuilder.Entity("InfoTabloServer.Models.Para", b =>
                {
                    b.HasOne("InfoTabloServer.Models.TimeShedule", "TimeShedule")
                        .WithMany("Paras")
                        .HasForeignKey("idTimeShedule")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfoTabloServer.Models.TypeInterval", "TypeInterval")
                        .WithMany("Paras")
                        .HasForeignKey("idTypeInterval")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TimeShedule");

                    b.Navigation("TypeInterval");
                });

            modelBuilder.Entity("MonthYearSupervisorShedule", b =>
                {
                    b.HasOne("InfoTabloServer.Models.MonthYear", null)
                        .WithMany()
                        .HasForeignKey("MonthYearsidMonthYear")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfoTabloServer.Models.SupervisorShedule", null)
                        .WithMany()
                        .HasForeignKey("SupervisorShedulesidSupervisorShedule")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InfoTabloServer.Models.AdditionalLessonsModels.DayWeek", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("InfoTabloServer.Models.AdditionalLessonsModels.SheduleAdditionalLesson", b =>
                {
                    b.Navigation("Times");
                });

            modelBuilder.Entity("InfoTabloServer.Models.AdditionalLessonsModels.Time", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("InfoTabloServer.Models.SupervisorShedule", b =>
                {
                    b.Navigation("DatesSupervisiors");
                });

            modelBuilder.Entity("InfoTabloServer.Models.TimeShedule", b =>
                {
                    b.Navigation("Paras");
                });

            modelBuilder.Entity("InfoTabloServer.Models.TypeInterval", b =>
                {
                    b.Navigation("Paras");
                });
#pragma warning restore 612, 618
        }
    }
}
