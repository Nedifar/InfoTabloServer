using InfoTablo.Domain.AdditionalLessonsModels;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class ScheduleAdditionalLessonConfiguration
        : IEntityTypeConfiguration<ScheduleAdditionalLesson>
    {
        public void Configure(EntityTypeBuilder<ScheduleAdditionalLesson> builder)
        {
            builder.HasKey(p => p.IdSheduleAdditionalLesson);
            builder.HasIndex(p => p.IdSheduleAdditionalLesson).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.DurationLesson).IsRequired();
        }
    }
}
