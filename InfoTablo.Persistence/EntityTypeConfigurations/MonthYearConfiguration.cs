using InfoTablo.Domain;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class MonthYearConfiguration : IEntityTypeConfiguration<MonthYear>
    {
        public void Configure(EntityTypeBuilder<MonthYear> builder)
        {
            builder.HasKey(p => p.IdMonthYear);
            builder.HasIndex(p=>p.IdMonthYear).IsUnique();
        }
    }
}
