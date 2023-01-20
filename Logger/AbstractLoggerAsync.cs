namespace Logger
{
    /// <summary>
    /// An async logging interface for avoid the code duplications.
    /// </summary>
    public abstract class AbstractLoggerAsync
    {
        /// <summary>
        /// The heart of the logging logic.
        /// </summary>
        /// <param name="type">The type of the log.</param>
        /// <param name="message">The message of the log.</param>
        /// <returns>True if everything was success, false otherwise.</returns>
        public abstract Task<bool> Log(LogType type, string? message);
        
        /// <summary>
        /// Logs only the info type message to console/file/stream.
        /// </summary>
        /// <param name="message">The message of the log</param>
        /// <returns>True if success, false otherwise.</returns>
        public async Task<bool> Info(string? message)
        {
            try
            {
                return await Log(LogType.info, message);
            }
            catch (Exception)
            {                
            }

            return false;
        }

        /// <summary>
        /// Logs only the debug type message to console/file/stream.
        /// </summary>
        /// <param name="message">The message of the log</param>
        /// <returns>True if success, false otherwise.</returns>
        public async Task<bool> Debug(string? message)
        {
            try
            {
                return await Log(LogType.debug, message);
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// Logs only the error type message to console/file/stream.
        /// </summary>
        /// <param name="message">The message of the log</param>
        /// <returns>True if success, false otherwise.</returns>
        public async Task<bool> Error(string? message)
        {
            try
            {
                 return await Log(LogType.error, message);
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}
