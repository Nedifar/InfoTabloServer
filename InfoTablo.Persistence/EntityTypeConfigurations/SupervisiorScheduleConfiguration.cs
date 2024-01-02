using InfoTablo.Domain;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class SupervisiorScheduleConfiguration : IEntityTypeConfiguration<SupervisiorSchedule>
    {
        public void Configure(EntityTypeBuilder<SupervisiorSchedule> builder)
        {
            builder.HasKey(p => p.IdSupervisiorSchedule);
            builder.HasIndex(p=>p.IdSupervisiorSchedule).IsUnique();
            builder.Property(p => p.NameSupervisior).HasMaxLength(50)
                .IsRequired();
            builder.Property(p=>p.Position).HasMaxLength(50)
                .IsRequired();
        }
    }
}
