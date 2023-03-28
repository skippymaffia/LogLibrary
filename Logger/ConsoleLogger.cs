using Logger.Common;

namespace Logger;

public class ConsoleLogger: ILogger
{
    private readonly object _lock = new object();
    private static readonly SemaphoreSlim semaphoreSlim = new(1, 1);

    public ConsoleLogger()
    {
    }

    /// <summary>
    /// Logs the different types of messages to the console.
    /// </summary>
    /// <param name="type">The type of the log.</param>
    /// <param name="message">The message of the log.</param>
    /// <returns>True if the log success, false otherwise.</returns>
    public bool Log(LogType type, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        if (message.Length > ILogger.LogLength)
        {
            throw new Exception("The length of the message is too long!");
        }

        lock (_lock)
        {
            DoLog(type, message);
        }

        return true;
    }

    public async Task<bool> LogAsync(LogType type, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        if (message.Length > ILogger.LogLength)
        {
            throw new Exception("The length of the message is too long!");
        }

        await semaphoreSlim.WaitAsync();
        try
        {
            return await DoLogAsync(type, message);
        }
        finally 
        { 
            semaphoreSlim?.Release();
        }
    }

    private void DoLog(LogType type, string? message)
    {
        var fgc = Console.ForegroundColor;
        Console.ForegroundColor = Helper.GetForegroundColor(type);
        Console.WriteLine($"{DateTime.Now} [{type}] {message}");
        Console.ForegroundColor = fgc;
    }

    private async Task<bool> DoLogAsync(LogType type, string? message)
    {
        await Task.Run(() => 
        { 
            DoLog(type, message); 
        });

        return true;
    }
}
