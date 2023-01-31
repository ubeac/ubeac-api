using Microsoft.Extensions.Hosting;

namespace uBeac.BackgroundTasks;

// The default behavior of the BackgroundService is that StartAsync calls ExecuteAsync.
// It's a default, the StartAsync is virtual so you could override it.
// If you create a subclass of BackgroundService, you must implement ExecuteAsync
// (because it's abstract). That should do your work.
// https://stackoverflow.com/questions/60356396/difference-between-executeasync-and-startasync-methods-in-backgroundservice-net#:~:text=The%20default%20behavior%20of%20the,so%20you%20could%20override%20it.&text=If%20you%20create%20a%20subclass,That%20should%20do%20your%20work.
public class BackgroundTaskManager : BackgroundService
{
    private readonly IEnumerable<IRecurringBackgroundTask> _backgroundTasks;

    public BackgroundTaskManager(IEnumerable<IRecurringBackgroundTask> backgroundTasks)
    {
        _backgroundTasks = backgroundTasks;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        foreach (var backgroundTask in _backgroundTasks)
        {
            await backgroundTask.Start(cancellationToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var backgroundTask in _backgroundTasks)
        {
            await backgroundTask.Stop(cancellationToken);
        }
        await base.StopAsync(cancellationToken);
    }

    public override void Dispose()
    {
        foreach (var backgroundTask in _backgroundTasks)
        {
            backgroundTask.Dispose();
        }
        base.Dispose();
    }
}
