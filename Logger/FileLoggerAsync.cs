namespace Logger
{
    public class FileLoggerAsync:AbstractLoggerAsync
    {
        private const int SIZE = 5000;

        private string dir;
        private string fileName = "skippy-log";
        private string fileDotExt = ".txt";
        private string filePath;
        private int fileSize;


        public FileLoggerAsync(string directory)
        {
            dir = directory;
            fileSize = 0;
            filePath = string.Format(@"{0}{1}{2}", dir, fileName, fileDotExt);
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
            if (!File.Exists(filePath))
            {
                WriteAsync(msg);
            }
            else
            {
                fileSize = fileSize == 0 ? File.ReadAllText(filePath).Length : fileSize;
                if (fileSize + msg.Length < SIZE)
                {
                    WriteAsync(msg, isAppend: true);
                }
                else
                {
                    //Console.WriteLine("archiveing...");
                    Archive();
                    WriteAsync(msg);
                }
            }

            return Task.FromResult(true);
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
            fileSize += message.Length + 2; //+2 cause cr+lf on win
        }

        /// <summary>
        /// The archive method which renames the "full" file to an archive name.
        /// </summary>
        public void Archive()
        {
            string destination = Helper.GetDestinationFileName(dir, fileName, fileDotExt);
            //Console.WriteLine("path:"+filePath+":destination:"+destination);
            File.Move(filePath, destination);
            fileSize = 0;
        }       
    }
}