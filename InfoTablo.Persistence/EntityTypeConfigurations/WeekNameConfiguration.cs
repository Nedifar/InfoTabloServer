using InfoTablo.Domain;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    class WeekNameConfiguration : IEntityTypeConfiguration<WeekName>
    {
        public void Configure(EntityTypeBuilder<WeekName> builder)
        {
            builder.HasKey(p => p.IdWeekName);
            builder.HasIndex(p => p.IdWeekName).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(60)
                .IsRequired();
        }
    }
}
