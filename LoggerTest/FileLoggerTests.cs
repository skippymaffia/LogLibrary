namespace Logger.Tests
{
    [TestClass]
    public class FileLoggerTests
    {
        [TestMethod]
        public void TestAllSuccess()
        {
            ILogger logger = new FileLogger(@"c:\temp\");
            string s = Util.GetMoreThan1000LongInput();
            string[] msgs = { "alpha", "beta", "gamma", s };
            foreach (var msg in msgs)
            {
                Assert.IsTrue(logger.Info(msg));
                Assert.IsTrue(logger.Debug(msg));
                Assert.IsTrue(logger.Error(msg));
            }
        }

        [TestMethod]
        public void TestAllUnSuccess()
        {
            ILogger logger = new FileLogger(@"c:\temp\");
            string[] msgs = { string.Empty, "     ", null };
            foreach (var msg in msgs)
            {
                Assert.IsFalse(logger.Info(msg));
                Assert.IsFalse(logger.Debug(msg));
                Assert.IsFalse(logger.Error(msg));
            }
        }
    }
}