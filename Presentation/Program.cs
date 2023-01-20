using Logger;

namespace Presentation
{
    internal class Program
    {
        internal static string GetSimpleContent(int i, int j)
        {
            return j switch
            {
                1 => string.Format("This is the simple {0}. console log content", i),
                2 => string.Format("This is the simple {0}. file log content", i),
                _ => string.Format("This is the simple {0}. stream log content", i),
            };
        }

        static void Main(string[] args)
        {
            //-------------
            ILogger logger = new ConsoleLogger();
            AbstractLoggerAsync loggerAsync = new ConsoleLoggerAsync();
            string log;
            for (int i = 0; i < 2; i++)
            {
                log = GetSimpleContent(i, 1);
                //logger.Info(log);
                //logger.Debug(log);
                //logger.Error(log);

                loggerAsync.Info(log).GetAwaiter().GetResult();
                loggerAsync.Debug(log).GetAwaiter().GetResult();
                loggerAsync.Error(log).GetAwaiter().GetResult();
            }

            //-------------
            string dir = @"c:\temp\";
            //logger = new FileLogger(directory:dir);
            loggerAsync = new FileLoggerAsync(directory:dir);
            for (int i = 0; i < 150; i++)
            {
                log = GetSimpleContent(i, 2);
                //logger.Info(log);
                //logger.Debug(log);
                //logger.Error(log);

                loggerAsync.Info(log).GetAwaiter().GetResult();
                loggerAsync.Debug(log).GetAwaiter().GetResult();
                loggerAsync.Error(log).GetAwaiter().GetResult();
            }

            string[] files = Directory.GetFiles(dir, "skippy-*");
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }

            //-------------
            log = GetSimpleContent(1, 3);
            using (MemoryStream stream = new MemoryStream())
            {
                //logger = new StreamLogger(stream);
                loggerAsync = new StreamLoggerAsync(stream);
                //logger.Info(log);
                //logger.Debug(log);
                //logger.Error(log);

                loggerAsync.Info(log).GetAwaiter().GetResult();
                loggerAsync.Debug(log).GetAwaiter().GetResult();
                loggerAsync.Error(log).GetAwaiter().GetResult();

                stream.Position = 0;
                var reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    var str = reader.ReadLine();
                    Console.WriteLine(str);
                }
            }

            log = GetSimpleContent(2, 3);
            var fileName = @"C:\temp\tmp-skippy.txt";
            using (FileStream stream = File.Create(fileName))
            {
                //logger = new StreamLogger(stream);
                loggerAsync = new StreamLoggerAsync(stream);
                //logger.Info(log);
                //logger.Debug(log);
                //logger.Error(log);

                loggerAsync.Info(log).GetAwaiter().GetResult();
                loggerAsync.Debug(log).GetAwaiter().GetResult();
                loggerAsync.Error(log).GetAwaiter().GetResult();

                stream.Position = 0;
                var reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    var str = reader.ReadLine();
                    Console.WriteLine(str);
                }
            }
            
            //cleaning
            /*File.Delete(fileName);
            foreach (var file in files) 
            {
                File.Delete(file); 
            }*/

            Console.WriteLine("The End!");
        }
    }
}