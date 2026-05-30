using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configuration;

public class BookCopiesConfiguration : IEntityTypeConfiguration<BookCopy>
{
    public void Configure(EntityTypeBuilder<BookCopy> builder)
    {
        builder.ToTable("BookCopies");
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.Book)
            .WithMany(b => b.Copies)
            .HasForeignKey(b => b.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(b => b.IsAvailable)
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(b => b.CreatedOn)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()")
            .IsRequired();

        builder.Property(b => b.SerialNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.IsDeleted)
        .HasColumnType("bit")
        .HasDefaultValue(false)
        .IsRequired();
    }
}