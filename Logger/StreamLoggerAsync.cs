using System.Text;

namespace Logger
{
    public class StreamLoggerAsync:AbstractLoggerAsync, IDisposable
    {
        private readonly Stream _stream;

        public StreamLoggerAsync(Stream stream)
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
        public override Task<bool> Log(LogType type, string? message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return Task.FromResult(false);
            }

            string msg = $"{DateTime.Now} [{type}] {message}";
            var writer = new StreamWriter(_stream, Encoding.UTF8);
            //Console.WriteLine("pos:" + stream.Position);
            writer.WriteLine(msg);
            writer.Flush();

            return Task.FromResult(true);
        }        
    }
}