using Core.InfoTablo.Domain;
using InfoTablo.Application.Interfaces;
using InfoTablo.Domain;
using InfoTablo.Domain.AdditionalLessonsModels;
using InfoTablo.Persistence.EntityTypeConfigurations;

namespace InfoTablo.Persistence
{
    public class InfoTabloDbContext : DbContext, IInfoTabloDbContext
    {
        public InfoTabloDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Announcment> Announcments { get; set; }
        public DbSet<DatesSupervisior> DatesSupervisiors { get; set; }
        public DbSet<DayPartHeader> DayPartHeaders { get; set; }
        public DbSet<MonthYear> MonthYears { get; set; }
        public DbSet<Para> Paras { get; set; }
        public DbSet<SpecialBackgroundPhoto> SpecialBackgroundPhotos { get; set; }
        public DbSet<SpecialDayWeekName> SpecialDayWeekNames { get; set; }
        public DbSet<SupervisiorSchedule> SupervisiorSchedules { get; set; }
        public DbSet<TimeSchedule> TimeSchedules { get; set; }
        public DbSet<TypeInterval> TypeIntervals { get; set; }
        public DbSet<WeekName> WeekNames { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<DayWeek> DayWeeks { get; set; }
        public DbSet<ScheduleAdditionalLesson> ScheduleAdditionalLessons { get; set; }
        public DbSet<Time> Times { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnnouncmentConfiguration());
            modelBuilder.ApplyConfiguration(new DatesSupervisiorConfiguration());
            modelBuilder.ApplyConfiguration(new DayPartHeaderConfiguration());
            modelBuilder.ApplyConfiguration(new MonthYearConfiguration());
            modelBuilder.ApplyConfiguration(new ParaConfiguration());
            modelBuilder.ApplyConfiguration(new SpecialBackgroundPhotoConfiguration());
            modelBuilder.ApplyConfiguration(new SpecialDayWeekNameConfiguration());
            modelBuilder.ApplyConfiguration(new SupervisiorScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new TimeScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new TimeScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new WeekNameConfiguration());
            modelBuilder.ApplyConfiguration(new TimeConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleAdditionalLessonConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new DayWeekConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
