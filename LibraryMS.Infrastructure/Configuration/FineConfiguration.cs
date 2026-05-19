using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configuration;

public class FineConfiguration : IEntityTypeConfiguration<Fine>
{
    public void Configure(EntityTypeBuilder<Fine> builder)
    {
        builder.ToTable("Fines");
        builder.HasKey(f => f.Id);
        
        builder.HasOne(f => f.Client)
            .WithMany(c => c.Fines)
            .HasForeignKey(f => f.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(f => f.BorrowingRecord)
            .WithOne(br => br.Fine)
            .HasForeignKey<Fine>(f => f.BorrowingRecordId) 
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(f => f.NumberOfLateDays)
            .HasColumnType("int")
            .HasDefaultValue(0)
            .IsRequired();
        
        builder.Property(f => f.FineAmount)
            .HasColumnType("decimal(5,2)")
            .IsRequired();
        
        builder.Property(f => f.PaymentStatus)
            .HasColumnType("smallint")
            .IsRequired();
        
        builder.Property(f => f.Reason)
            .HasColumnType("nvarchar(500)")
            .HasMaxLength(500)
            .IsRequired();
    }
}