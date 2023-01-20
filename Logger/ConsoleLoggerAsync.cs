namespace Logger
{
    public class ConsoleLoggerAsync : AbstractLoggerAsync
    {
        public ConsoleLoggerAsync()
        {
        }

        /// <summary>
        /// Logs the different types of messages to the console.
        /// </summary>
        /// <param name="type">The type of the log.</param>
        /// <param name="message">The message of the log.</param>
        /// <returns>True if the log success, false otherwise.</returns>
        public override Task<bool> Log(LogType type, string? message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return Task.FromResult(false);
            }

            if (message.Length > 1000)
            {
                throw new Exception("The length of the message is too long!");
            }

            var fgc = Console.ForegroundColor;
            Console.ForegroundColor = Helper.GetForegroundColor(type);
            Console.WriteLine($"{DateTime.Now} [{type}] {message}");
            Console.ForegroundColor = fgc;

            return Task<bool>.FromResult(true);
        }
    }
}
