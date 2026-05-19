namespace LibraryMS.Domain.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string RefreshTokenJwt { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiry { get; set; }
    public bool IsRevoked { get; set; } = false;
    public DateTime? RevokedAt { get; set; }
    
}