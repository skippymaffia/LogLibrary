namespace Logger;

/// <summary>
/// An async logging interface for avoid the code duplications.
/// </summary>
public abstract class AbstractAsyncLogger
{
    public const int LogLength = 1000;

    /// <summary>
    /// The heart of the logging logic.
    /// </summary>
    /// <param name="type">The type of the log.</param>
    /// <param name="message">The message of the log.</param>
    /// <returns>True if everything was success, false otherwise.</returns>
    public abstract Task<bool> Log(LogType type, string? message);

    public async Task<bool> TryLog(LogType type, string? message)
    {
        try
        {
            return await Log(type, message);
        }
        catch (Exception)
        {
        }

        return false;
    }

    /// <summary>
    /// Logs only the info type message to console/file/stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public async Task<bool> Info(string? message)
    {
        return await TryLog(LogType.info, message);
    }

    /// <summary>
    /// Logs only the debug type message to console/file/stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public async Task<bool> Debug(string? message)
    {
        return await TryLog(LogType.debug, message);
    }

    /// <summary>
    /// Logs only the error type message to console/file/stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public async Task<bool> Error(string? message)
    {        
        return await TryLog(LogType.error, message);
    }

    /// <summary>
    /// Logs only the warning type message to console/file/stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public async Task<bool> Warning(string? message)
    {
        return await TryLog(LogType.warning, message);
    }
}
