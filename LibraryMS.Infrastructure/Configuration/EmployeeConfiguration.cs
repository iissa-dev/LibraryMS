namespace LibraryMS.Infrastructure.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(a => a.Id);
        builder.HasIndex(e => e.PersonId).IsUnique();

        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasOne(e => e.Person)
            .WithOne()
            .HasForeignKey<Employee>(e => e.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.EmployeeCode)
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.IsDeleted)
            .HasColumnType("bit")
            .HasDefaultValue(0)
            .IsRequired();
    }
}