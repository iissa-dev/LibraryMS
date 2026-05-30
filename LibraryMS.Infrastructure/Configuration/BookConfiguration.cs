using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.ToTable("Books");
        builder.HasQueryFilter(b => !b.IsDeleted);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.ISBN)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.PublishDate)
            .IsRequired();

        builder.Property(b => b.Genre)
            .HasDefaultValue(Genre.Other)
            .IsRequired();


        builder.Property(b => b.AdditionalDetails)
            .HasMaxLength(1000)
            .IsRequired(false);


        builder.Property(b => b.BookImageUrl)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(b => b.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasMany(b => b.BookAuthors)
            .WithOne(ba => ba.Book)
            .HasForeignKey(ba => ba.BookId);

        builder.HasMany(b => b.Copies)
            .WithOne(bc => bc.Book)
            .HasForeignKey(bc => bc.BookId);

        builder.HasMany(b => b.Reservations)
            .WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId);
    }
}