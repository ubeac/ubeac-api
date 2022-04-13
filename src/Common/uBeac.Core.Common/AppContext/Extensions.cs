﻿namespace uBeac;

public static class Extensions
{
    public static ApplicationContextModel ToModel(this IApplicationContext appContext)
        => new()
        {
            TraceId = appContext.TraceId,
            SessionId = appContext.SessionId,
            UserName = appContext.UserName,
            UserIp = appContext.UserIp,
            Language = appContext.Language
        };
}