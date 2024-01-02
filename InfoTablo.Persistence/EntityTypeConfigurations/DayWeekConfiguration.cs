using InfoTablo.Domain.AdditionalLessonsModels;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class DayWeekConfiguration : IEntityTypeConfiguration<DayWeek>
    {
        public void Configure(EntityTypeBuilder<DayWeek> builder)
        {
            builder.HasKey(p => p.IdDayWeek);
            builder.HasIndex(p => p.IdDayWeek).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(20)
                .IsRequired();
        }
    }
}
