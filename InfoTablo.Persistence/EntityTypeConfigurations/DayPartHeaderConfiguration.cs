using InfoTablo.Domain;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class DayPartHeaderConfiguration : IEntityTypeConfiguration<DayPartHeader>
    {
        public void Configure(EntityTypeBuilder<DayPartHeader> builder)
        {
            builder.HasKey(p => p.DayPartHeaderId);
            builder.HasIndex(p=>p.DayPartHeaderId).IsUnique();
            builder.Property(p=>p.Header).HasMaxLength(40);
        }
    }
}
