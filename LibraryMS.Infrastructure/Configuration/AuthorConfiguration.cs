using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configuration;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);
        builder.ToTable("Authors");
        
        builder.HasOne(a => a.Person)
            .WithOne()
            .HasForeignKey<Author>(a => a.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(a => a.Biography)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();
    }
}