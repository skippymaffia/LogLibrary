namespace Logger.Tests
{
    [TestClass]
    public class FileLoggerAsyncTests
    {
        [TestMethod]
        public void TestAllSuccess()
        {
            AbstractLoggerAsync logger = new FileLoggerAsync(@"c:\temp\");
            string s = Util.GetMoreThan1000LongInput();
            string[] msgs = { "alpha", "beta", "gamma", s };
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
            AbstractLoggerAsync logger = new FileLoggerAsync(@"c:\temp\");
            string[] msgs = { string.Empty, "     ", null };
            foreach (var msg in msgs)
            {
                Assert.IsFalse(logger.Info(msg).GetAwaiter().GetResult());
                Assert.IsFalse(logger.Debug(msg).GetAwaiter().GetResult());
                Assert.IsFalse(logger.Error(msg).GetAwaiter().GetResult());
            }
        }
    }
}