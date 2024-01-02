using InfoTablo.Domain;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class DatesSupervisiorConfiguration : IEntityTypeConfiguration<DatesSupervisior>
    {
        public void Configure(EntityTypeBuilder<DatesSupervisior> builder)
        {
            builder.HasKey(p => p.IdDatesSupervisior);
            builder.HasIndex(p=>p.IdDatesSupervisior).IsUnique();
            builder.HasOne(d => d.SupervisorSchedule)
                .WithMany(s => s.DatesSupervisiors)
                .HasForeignKey(d => d.IdSupervisorShedule);
        }
    }
}
