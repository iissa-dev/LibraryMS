using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configuration;

public class PeopleConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("People");
        builder.HasKey(f => f.Id);
        
        builder.HasOne(p => p.Country)
            .WithMany()
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(p => p.FirstName)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(p => p.LastName)
            .HasMaxLength(50)
            .IsRequired(); 
        
        builder.Property(p => p.Address)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(p => p.DateOfBirth)
            .HasColumnType("date")
            .IsRequired();
        
        builder.Property(p => p.ImageUrl)
            .HasColumnType("nvarchar(500)")
            .HasMaxLength(500)
            .IsRequired(false);
    }
}