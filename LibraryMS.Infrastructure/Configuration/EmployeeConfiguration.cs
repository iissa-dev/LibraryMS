using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(a => a.Id);

        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<Employee>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.UserId)
            .IsUnique();

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