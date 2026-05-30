namespace LibraryMS.Infrastructure.Configuration;

public class BookAuthorsConfiguration: IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        builder.ToTable("BookAuthors");
        builder.HasKey(b => b.Id);

        builder.HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(ba=> ba.CreatedOn)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()")
            .IsRequired();
    }
}