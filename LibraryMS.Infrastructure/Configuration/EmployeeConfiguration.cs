using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(a => a.Id);
        
        builder.HasOne(e => e.Person)
            .WithOne()
            .HasForeignKey<Employee>(e => e.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(e => e.EmployeeCode)
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50)
            .IsRequired();
    }
}