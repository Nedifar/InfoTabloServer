using InfoTablo.Domain;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class TypeIntervalConfiguration : IEntityTypeConfiguration<TypeInterval>
    {
        public void Configure(EntityTypeBuilder<TypeInterval> builder)
        {
            builder.HasKey(p => p.IdInterval);
            builder.HasIndex(p => p.IdInterval).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(40)
                .IsRequired();
        }
    }
}
