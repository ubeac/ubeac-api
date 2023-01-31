namespace uBeac.BackgroundTasks;

public interface IRecurringBackgroundTask : IDisposable
{
    public Task Process(CancellationToken cancellationToken = default);
    public Task Start(CancellationToken cancellationToken = default);
    public Task Stop(CancellationToken cancellationToken = default);
}

public abstract class RecurringBackgroundTask : IRecurringBackgroundTask
{
    private Timer _timer = null;

    public RecurringBackgroundTaskOption Option { get; }

    protected RecurringBackgroundTask(RecurringBackgroundTaskOption option)
    {
        Option = option;
    }

    public virtual Task Start(CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            DisableTimer();
        else
            EnableTimer();

        return Task.CompletedTask;
    }

    public virtual Task Stop(CancellationToken cancellationToken = default)
    {
        DisableTimer();
        return Task.CompletedTask;
    }

    public abstract Task Process(CancellationToken cancellationToken = default);

    public virtual void Dispose()
    {
        _timer?.Dispose();
    }

    private void TimerElapsed(object state)
    {
        Process().Wait();
    }

    private void EnableTimer()
    {
        var durationToStart = TimeSpan.Zero;
        var recurringDuration = Timeout.InfiniteTimeSpan;

        if (Option.Start.HasValue)
        {
            var startTime = DateTime.Today + Option.Start.Value;

            if (startTime < DateTime.Now && Option.Recurring.HasValue)
            {
                while (startTime < DateTime.Now)
                    startTime += Option.Recurring.Value;
            }

            durationToStart = (DateTime.Now - startTime).Duration();

        }

        if (Option.Recurring.HasValue)
            recurringDuration = Option.Recurring.Value;

        _timer = new Timer(TimerElapsed, null, durationToStart, recurringDuration);
    }

    private void DisableTimer()
    {
        _timer?.Change(Timeout.Infinite, 0);
    }
}
