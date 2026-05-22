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
        
        builder.Property(e => e.EmployeeCode)
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50)
            .IsRequired();
    }
}