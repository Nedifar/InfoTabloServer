using Core.InfoTablo.Domain;
using InfoTablo.Domain;
using InfoTablo.Domain.AdditionalLessonsModels;
using Microsoft.EntityFrameworkCore;

namespace InfoTablo.Application.Interfaces
{
    public interface IInfoTabloDbContext
    {
        DbSet<Announcment> Announcments { get; set; }
        DbSet<DatesSupervisior> DatesSupervisiors { get; set; }
        DbSet<DayPartHeader> DayPartHeaders { get; set; }
        DbSet<MonthYear> MonthYears { get; set; }
        DbSet<Para> Paras { get; set; }
        DbSet<SpecialBackgroundPhoto> SpecialBackgroundPhotos { get; set; }
        DbSet<SpecialDayWeekName> SpecialDayWeekNames { get; set; }
        DbSet<SupervisiorSchedule> SupervisiorSchedules { get; set; }
        DbSet<TimeSchedule> TimeSchedules { get; set; }
        DbSet<TypeInterval> TypeIntervals { get; set; }
        DbSet<WeekName> WeekNames { get; set; }
        DbSet<Lesson> Lessons { get; set; }
        DbSet<DayWeek> DayWeeks { get; set; }
        DbSet<ScheduleAdditionalLesson> ScheduleAdditionalLessons { get; set; }
        DbSet<Time> Times { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
