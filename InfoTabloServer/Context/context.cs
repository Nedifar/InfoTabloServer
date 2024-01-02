using InfoTabloServer.Models;
using InfoTabloServer.Models.AdditionalLessonsModels;
using Microsoft.EntityFrameworkCore;

namespace InfoTabloServer.Context
{
    public class context : DbContext
    {
        public context(DbContextOptions options) : base(options)
        {

        }

        public DbSet<WeekName> WeekNames { get; set; }
        public DbSet<TimeShedule> TimeShedules { get; set; }
        public DbSet<SupervisorShedule> SupervisorShedules { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<DatesSupervisior> DatesSupervisiors { get; set; }
        public DbSet<MonthYear> MonthYear { get; set; }
        public DbSet<DayPartHeader> DayPartHeaders { get; set; }
        public DbSet<SpecialDayWeekName> SpecialDayWeekNames { get; set; }
        public DbSet<TypeInterval> TypeIntervals { get; set; }
        public DbSet<Para> Paras { get; set; }
        public DbSet<SpecialBackgroundPhoto> SpecialBackgroundPhotos { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<SheduleAdditionalLesson> SheduleAdditionalLessons { get; set; }
        public DbSet<DayWeek> DayWeeks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();
        }
    }
}
