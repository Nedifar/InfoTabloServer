using InfoTablo.Domain.AdditionalLessonsModels;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class TimeConfiguration : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            builder.HasKey(p => p.IdTime);
            builder.HasIndex(p => p.IdTime).IsUnique();
            builder.Property(p=>p.BeginTime).IsRequired();

            builder.HasOne(p => p.ScheduleAdditionalLesson)
                .WithMany(t => t.Times)
                .HasForeignKey(p => p.IdScheduleAdditionalLesson);
        }
    }
}
