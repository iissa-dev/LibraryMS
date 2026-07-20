namespace LibraryMS.Infrastructure.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(a => a.Id);

        builder.HasIndex(c => c.PersonId);

        builder.HasQueryFilter(c => !c.IsDeleted);

        builder.HasOne(c => c.Person)
            .WithOne()
            .HasForeignKey<Client>(c => c.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.LibraryCardNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.IsDeleted)
            .HasColumnType("bit")
            .HasDefaultValue(false)
            .IsRequired();
    }
}