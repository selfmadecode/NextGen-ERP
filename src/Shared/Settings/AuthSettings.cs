namespace Shared.Settings;

public record AuthSettings
{
    public string SecretKey { get; init; }
    public string Issuer { get; init; }
}
