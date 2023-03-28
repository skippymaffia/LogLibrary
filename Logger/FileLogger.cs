using Logger.Common;

namespace Logger;

public class FileLogger:ILogger
{
    private readonly object _lock = new object();
    private static SemaphoreSlim semaphoreSlim = new(1, 1);

    private const int FILESIZE = 5000;

    private string dir;
    private string fileName = "log-" + Environment.MachineName;
    private string fileDotExt = ".txt";
    private string filePath;
    private int currFileSize;

    public FileLogger(string directory)
    {
        dir = directory;
        currFileSize = 0;
        filePath = string.Format(@"{0}{1}{2}", dir, fileName, fileDotExt);
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
            if (File.Exists(filePath))
            {
                Append(msg);
            }
            else
            {
                Write(msg);
            }
        }

        return true;
    }

    /// <summary>
    /// Writes/Appends a log into a file.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    /// <param name="isAppend">True if the message need to be appended, false otherwise.</param>
    /// <returns></returns>
    public void Write(string message, bool isAppend = false)
    {
        using StreamWriter file = new(filePath, append: isAppend);
        file.WriteLine(message);
        currFileSize += message.Length + 2; //+2 cause cr+lf on win
    }

    /// <summary>
    /// Writes/Appends a log into a file.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    /// <param name="isAppend">True if the message need to be appended, false otherwise.</param>
    /// <returns></returns>
    public async Task WriteAsync(string message, bool isAppend = false)
    {
        using StreamWriter file = new(filePath, append: isAppend);
        await file.WriteLineAsync(message);
        currFileSize += message.Length + 2; //+2 cause cr+lf on win
    }

    /// <summary>
    /// The archive method which renames the "full" file to an archive name.
    /// </summary>
    public void Archive()
    {
        string destination = Helper.GetDestinationFileName(dir, fileName, fileDotExt);
        //Console.WriteLine("path:"+filePath+":destination:"+destination);
        File.Move(filePath, destination);
        currFileSize = 0;
    }

    private void Append(string msg)
    {
        currFileSize = currFileSize == 0 ? File.ReadAllText(filePath).Length : currFileSize;
        var isAppend = true;
        if (currFileSize + msg.Length > FILESIZE)
        {
            Archive();
            isAppend = false;
        }

        Write(msg, isAppend);
    }

    private async Task AppendAsync(string msg)
    {
        currFileSize = currFileSize == 0 ? File.ReadAllText(filePath).Length : currFileSize;
        var isAppend = true;
        if (currFileSize + msg.Length > FILESIZE)
        {
            Archive();
            isAppend = false;
        }

        await WriteAsync(msg, isAppend);
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
            if (File.Exists(filePath))
            {
                await AppendAsync(msg);
            }
            else
            {
                await WriteAsync(msg);
            }
        }
        finally
        {
            semaphoreSlim?.Release();
        }

        return true;
    }
}