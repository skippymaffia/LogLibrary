namespace Logger;

public class ConsoleLogger: ILogger
{
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

        DoLog(type, message);

        return true;
    }

    public Task<bool> LogAsync(LogType type, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return Task.FromResult(false);
        }

        if (message.Length > ILogger.LogLength)
        {
            throw new Exception("The length of the message is too long!");
        }

        DoLog(type, message);

        return Task<bool>.FromResult(true);
    }

    private static void DoLog(LogType type, string? message)
    {
        var fgc = Console.ForegroundColor;
        Console.ForegroundColor = Helper.GetForegroundColor(type);
        Console.WriteLine($"{DateTime.Now} [{type}] {message}");
        Console.ForegroundColor = fgc;
    }
}
