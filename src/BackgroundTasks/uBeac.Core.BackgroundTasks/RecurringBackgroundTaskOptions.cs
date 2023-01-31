namespace uBeac.BackgroundTasks;

public class RecurringBackgroundTaskOption
{
    public string Name { get; set; }
    public TimeSpan? Start { get; set; }
    public TimeSpan? Recurring { get; set; }
    public string Type { get; set; }
}
