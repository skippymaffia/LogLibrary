namespace Logger.Tests
{
    [TestClass()]
    public class ConsoleLoggerAsyncTests
    {
        [TestMethod]
        public void TestAllSuccess()
        {
            AbstractLoggerAsync logger = new ConsoleLoggerAsync();
            string[] msgs = { "alpha", "beta", "gamma" };
            foreach (var msg in msgs)
            {
                Assert.IsTrue(logger.Info(msg).GetAwaiter().GetResult());
                Assert.IsTrue(logger.Debug(msg).GetAwaiter().GetResult());
                Assert.IsTrue(logger.Error(msg).GetAwaiter().GetResult());
            }
        }

        [TestMethod]
        public void TestAllUnSuccess()
        {
            AbstractLoggerAsync logger = new ConsoleLoggerAsync();
            string s = Util.GetMoreThan1000LongInput();
            Console.WriteLine(s);
            string[] msgs = { string.Empty, "     ", null, s };
            foreach (var msg in msgs)
            {
                Assert.IsFalse(logger.Info(msg).GetAwaiter().GetResult());
                Assert.IsFalse(logger.Debug(msg).GetAwaiter().GetResult());
                Assert.IsFalse(logger.Error(msg).GetAwaiter().GetResult());
            }
        }
    }
}