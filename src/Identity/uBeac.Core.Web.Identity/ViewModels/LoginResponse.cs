﻿namespace uBeac.Web.Identity
{
    public class LoginResponse<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        public TUserKey Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiry { get; set; }
    }

    public class LoginResponse : LoginResponse<Guid>
    {

    }
}