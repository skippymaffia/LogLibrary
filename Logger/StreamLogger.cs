using System.Text;
using Logger.Common;

namespace Logger;

public class StreamLogger:ILogger, IDisposable
{
    private readonly Stream _stream;
    private readonly object _lock = new();
    private static SemaphoreSlim semaphoreSlim = new(1, 1);

    public StreamLogger(Stream stream)
    {
        _stream = stream;
    }
    
    public void Dispose()
    {
        _stream.Dispose();
    }

    /// <summary>
    /// The whole logging logic for file logger.
    /// </summary>
    /// <param name="type">The type of the log.</param>
    /// <param name="message">The message to be logged.</param>
    /// <returns>True if success, false otherwise.</returns>
    public bool Log(LogType type, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        string msg = $"{DateTime.Now} [{type}] {message}";
        lock (_lock)
        {
            return DoLog(msg);
        }
    }

    public async Task<bool> LogAsync(LogType type, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        string msg = $"{DateTime.Now} [{type}] {message}";
        await semaphoreSlim.WaitAsync();
        try
        {
            return await DoLogAsync(msg);
        }
        finally
        {
            semaphoreSlim?.Release();
        }
    }

    private bool DoLog(string msg)
    {
        var writer = new StreamWriter(_stream, Encoding.UTF8);
        writer.WriteLine(msg);
        writer.Flush();

        return true;
    }

    private async Task<bool> DoLogAsync(string msg)
    {
        await Task.Run(() =>
        {
            return DoLog(msg);
        });        
        
        return true;
    }
}