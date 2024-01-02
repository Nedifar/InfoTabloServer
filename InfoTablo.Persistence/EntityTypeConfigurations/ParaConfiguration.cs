using InfoTablo.Domain;

namespace InfoTablo.Persistence.EntityTypeConfigurations
{
    public class ParaConfiguration : IEntityTypeConfiguration<Para>
    {
        public void Configure(EntityTypeBuilder<Para> builder)
        {
            builder.HasKey(table => table.IdPara);
            builder.HasIndex(table => table.IdPara).IsUnique();
            builder.Property(table => table.Name).HasMaxLength(64);
            builder.HasOne(table => table.TypeInterval)
                .WithMany(table => table.Paras)
                .HasForeignKey(table => table.IdTypeInterval);
            builder.HasOne(p => p.TimeSchedule)
                .WithMany(t => t.Paras)
                .HasForeignKey(p => p.IdTimeSchedule);
        }
    }
}
