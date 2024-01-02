using InfoTablo.Domain;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class TimeScheduleConfiguration : IEntityTypeConfiguration<TimeSchedule>
    {
        public void Configure(EntityTypeBuilder<TimeSchedule> builder)
        {
            builder.HasKey(p => p.IdTimeSchedule);
            builder.HasIndex(p => p.IdTimeSchedule).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(60)
                .IsRequired();
        }
    }
}
