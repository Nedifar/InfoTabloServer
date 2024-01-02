using Core.InfoTablo.Domain;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class AnnouncmentConfiguration : IEntityTypeConfiguration<Announcment>
    {
        public void Configure(EntityTypeBuilder<Announcment> builder)
        {
            builder.HasKey(p => p.IdAnnouncement);
            builder.HasIndex(p=> p.IdAnnouncement).IsUnique();
            builder.Property(p => p.Header).HasMaxLength(50);
            builder.Property(p => p.Priority).HasMaxLength(50);
            builder.Property(p => p.Status).HasMaxLength(50);
            builder.Property(p => p.Name).HasMaxLength(200); 
        }
    }
}
