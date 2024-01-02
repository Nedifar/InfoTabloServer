using InfoTablo.Domain.AdditionalLessonsModels;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(p => p.IdLesson);
            builder.HasIndex(p => p.IdLesson).IsUnique();
            builder.Property(p => p.Cabinet).HasMaxLength(30);
            builder.Property(p => p.GroupName).HasMaxLength(20)
                .IsRequired();
            builder.Property(p => p.TeacherName).HasMaxLength(50)
                .IsRequired();

            builder.HasOne(p => p.Time)
                .WithMany(l => l.Lessons)
                .HasForeignKey(p => p.IdTime);
            builder.HasOne(p => p.DayWeek)
                .WithMany(l => l.Lessons)
                .HasForeignKey(p => p.IdDayWeek);
        }
    }
}
