namespace LibraryMS.Infrastructure.Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(f => f.Id);
        builder.HasIndex(u => u.PersonId).IsUnique();

        builder.HasQueryFilter(u => !u.IsDeleted);

        builder
        .HasOne(u => u.Person)
        .WithOne()
        .HasForeignKey<ApplicationUser>(u => u.PersonId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.IsDeleted)
            .HasColumnType("bit")
            .HasDefaultValue(0)
            .IsRequired();
    }
}