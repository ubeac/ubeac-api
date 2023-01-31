namespace uBeac.BackgroundTasks.Logging;

public class BackgroundTaskSearchRequest
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public string Term { get; set; }
}
