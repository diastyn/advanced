namespace Advanced.Shared.Settings;

public class JwtSetting
{
    public const string JwtSettings = nameof(JwtSettings);
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
}