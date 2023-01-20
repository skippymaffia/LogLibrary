namespace Logger;

/// <summary>
/// A logging interface for avoid the code duplications.
/// </summary>
public interface ILogger
{
    const int LogLength = 1000;

    /// <summary>
    /// The heart of the logging logic.
    /// </summary>
    /// <param name="type">The type of the log.</param>
    /// <param name="message">The message of the log.</param>
    /// <returns>True if everything was success, false otherwise.</returns>
    public bool Log(LogType type, string? message);

    /// <summary>
    /// Try to Log() the spec type message to console/file/stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    private bool TryLog(LogType type, string? message)
    {
        try
        {
            return Log(type, message);
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
    public bool Info(string? message)
    {
        return TryLog(LogType.info, message);
    }

    /// <summary>
    /// Logs only the debug type message to console/file/stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public bool Debug(string? message)
    {
        return TryLog(LogType.debug, message);
    }

    /// <summary>
    /// Logs only the error type message to console/file/stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public bool Error(string? message)
    {
        return TryLog(LogType.error, message);
    }

    /// <summary>
    /// Logs only the warning type message to console/file/stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public bool Warning(string? message)
    {
        return TryLog(LogType.warning, message);
    }

    #region ASYNC PART
    /// <summary>
    /// The heart of the logging logic.
    /// </summary>
    /// <param name="type">The type of the log.</param>
    /// <param name="message">The message of the log.</param>
    /// <returns>True if everything was success, false otherwise.</returns>
    public Task<bool> LogAsync(LogType type, string? message);

    public async Task<bool> TryLogAsync(LogType type, string? message)
    {
        try
        {
            return await LogAsync(type, message);
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
    public async Task<bool> InfoAsync(string? message)
    {
        return await TryLogAsync(LogType.info, message);
    }

    /// <summary>
    /// Logs only the debug type message to console/file/stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public async Task<bool> DebugAsync(string? message)
    {
        return await TryLogAsync(LogType.debug, message);
    }

    /// <summary>
    /// Logs only the error type message to console/file/stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public async Task<bool> ErrorAsync(string? message)
    {
        return await TryLogAsync(LogType.error, message);
    }

    /// <summary>
    /// Logs only the warning type message to console/file/stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public async Task<bool> WarningAsync(string? message)
    {
        return await TryLogAsync(LogType.warning, message);
    }
    #endregion ASYNC PART
}
