using InfoTablo.Domain;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class SpecialDayWeekNameConfiguration : IEntityTypeConfiguration<SpecialDayWeekName>
    {
        public void Configure(EntityTypeBuilder<SpecialDayWeekName> builder)
        {
            builder.HasKey(p => p.IdSpecialDayWeekName);
            builder.HasIndex(p => p.IdSpecialDayWeekName).IsUnique();
            builder.Property(p => p.DayWeek).IsRequired();
        }
    }
}
