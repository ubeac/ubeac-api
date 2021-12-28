﻿namespace uBeac.Identity
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double Expires { get; set; } = -1; // second, -1 means never expire
        public string Secret { get; set; } = string.Empty;
    }
}
