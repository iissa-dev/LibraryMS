using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(a => a.Id);
        
        builder.HasOne(c => c.Person)
            .WithOne()
            .HasForeignKey<Client>(c => c.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(c => c.LibraryCardNumber)
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50)
            .IsRequired();
    }
}