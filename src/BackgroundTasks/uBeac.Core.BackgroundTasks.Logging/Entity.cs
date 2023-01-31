namespace uBeac.BackgroundTasks.Logging;

public class BackgroundTaskLog : Entity
{
    public RecurringBackgroundTaskOption Option { get; set; }

    public List<string> Descriptions { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public BackgroundTaskStatus Status { get; set; }

    public int Succeed { get; set; }
    public int Failure { get; set; }
}

public enum BackgroundTaskStatus
{
    Idle = 1,
    Running = 2
}
