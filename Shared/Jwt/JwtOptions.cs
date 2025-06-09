namespace Shared.Jwt;

public class JwtOptions
{
    public string SecretKey { get; set; } = string.Empty;
    public double DurationInDays { get; set; }
}
