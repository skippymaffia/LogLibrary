namespace Logger.Tests
{
    [TestClass()]
    public class ConsoleLoggerTests
    {
        [TestMethod]
        public void TestAllSuccess()
        {
            ILogger logger = new ConsoleLogger();
            string[] msgs = { "alpha", "beta", "gamma" };
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
            ILogger logger = new ConsoleLogger();
            string s = Util.GetMoreThan1000LongInput();
            Console.WriteLine(s);
            string[] msgs = { string.Empty, "     ", null, s };
            foreach (var msg in msgs)
            {
                Assert.IsFalse(logger.Info(msg));
                Assert.IsFalse(logger.Debug(msg));
                Assert.IsFalse(logger.Error(msg));
            }
        }
    }
}