namespace uBeac.Web.Logging
{
    public class LogRequest
    {
        public string? Host { get; set; }
        public int? Port { get; set; }
        public string? Scheme { get; set; }
        public string? ContentType { get; set; }
        public long? ContentLength { get; set; }
        public string? QueryString { get; set; }
        public string? Method { get; set; }
        public string? Protocol { get; set; }
        public bool HasFormContentType { get; set; }
        public string? Path { get; set; }
        public Dictionary<string, List<string>>? Headers { get; set; }
        public long? Length { get; set; }
        public string? Ip { get; set; }
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
    }
}
