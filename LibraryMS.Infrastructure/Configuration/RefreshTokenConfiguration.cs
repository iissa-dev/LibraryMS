namespace LibraryMS.Infrastructure.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(a => a.Id);
        
        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(rt => rt.RefreshTokenJwt)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(rt => rt.RevokedAt)
            .HasColumnType("datetime2")
            .IsRequired(false);  
        
        builder.Property(rt => rt.RefreshTokenExpiry)
            .HasColumnType("datetime2")
            .IsRequired();
        
        builder.Property(rt => rt.IsRevoked)
            .HasDefaultValue(false)
            .IsRequired();
        
    }
}