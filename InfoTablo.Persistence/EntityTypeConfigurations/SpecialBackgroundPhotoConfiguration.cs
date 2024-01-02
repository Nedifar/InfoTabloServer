using InfoTablo.Domain;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class SpecialBackgroundPhotoConfiguration : IEntityTypeConfiguration<SpecialBackgroundPhoto>
    {
        public void Configure(EntityTypeBuilder<SpecialBackgroundPhoto> builder)
        {
            builder.HasKey(p => p.IdSpecialBackgroundPhoto);
            builder.Property(p => p.FileName).HasMaxLength(255);
            builder.Property(p=>p.FileName).IsRequired();
        }
    }
}
