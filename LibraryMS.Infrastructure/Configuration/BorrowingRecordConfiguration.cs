namespace LibraryMS.Infrastructure.Configuration;

public class BorrowingRecordConfiguration : IEntityTypeConfiguration<BorrowingRecord>
{
    public void Configure(EntityTypeBuilder<BorrowingRecord> builder)
    {
        builder.ToTable("BorrowingRecords");
        builder.HasKey(b => b.Id);
        
        builder.HasOne(b => b.Client)
            .WithMany(c => c.BorrowingRecords)
            .HasForeignKey(b => b.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(b => b.BookCopy)
            .WithMany(c => c.BorrowingRecords)
            .HasForeignKey(b => b.CopyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(b => b.BorrowingDate)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()")
            .IsRequired();

        builder.Property(b => b.DueDate)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(b => b.ActualReturnDate)
            .HasColumnType("datetime2")
            .IsRequired(false);
        
        builder.Property(b=> b.CreatedOn)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()")
            .IsRequired();
    }
}