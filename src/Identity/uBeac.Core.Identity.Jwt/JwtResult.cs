﻿namespace uBeac.Identity;

public class JwtResult
{
    public virtual string Token { get; set; }
    public virtual DateTime Expiry { get; set; }
}