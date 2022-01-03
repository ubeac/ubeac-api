namespace uBeac.Web.Logging
{
    public class ApiLog
    {
        public string? TraceId { get; set; } 
        public DateTime Date { get; set; }
        public double? Duration { get; set; } //milisecond
        public LogRequest? Request { get; set; } 
        public LogResponse? Response { get; set; } 
    }    
}
