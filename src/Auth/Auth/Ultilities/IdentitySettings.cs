namespace Auth.Ultilities;

public class IdentitySettings
{
    public int LockoutMinutes { get; set; }
    public int MinimumPasswordLength { get; set; }
    public int MaxLockoutAttempt { get; set; }
}
