namespace uBeac.Identity
{
    public class TwoFactorRecoveryCode
    {
        public string Code { get; set; } = string.Empty;
        public bool Redeemed { get; set; }
    }
}
