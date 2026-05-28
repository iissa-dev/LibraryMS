using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(a => a.Id);

        builder.HasOne<ApplicationUser>()
        .WithOne()
        .HasForeignKey<Client>(c => c.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => c.UserId)
            .IsUnique();
        
        builder.Property(c => c.LibraryCardNumber)
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50)
            .IsRequired();
    }
}