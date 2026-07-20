namespace LibraryMS.Application.DTOs.AuthDto;

public class TokenResult
{
    public int UserId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    [JsonIgnore]
    public string RefreshToken { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public int? ClientId { get; set; }
    public int PersonId { get; set; }
}
