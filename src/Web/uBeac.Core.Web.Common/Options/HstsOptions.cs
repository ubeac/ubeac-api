﻿namespace uBeac.Web;

public class HstsOptions
{
    public bool Preload { get; set; }
    public bool IncludeSubDomains { get; set; }
    public int MaxAge { get; set; } // days
    public IList<string> ExcludedHosts { get; set; }
}