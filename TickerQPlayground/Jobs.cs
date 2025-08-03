using TickerQ.Utilities.Base;

namespace TickerQPlayground;

public class Jobs
{
    [TickerFunction(functionName: "CleanerJob")]
    public void CleanupLogs()
    {
        Console.WriteLine("Cleaner...");
    }
}
