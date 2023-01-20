namespace Logger.Tests
{
    [TestClass()]
    public class StreamLoggerTests
    {
        [TestMethod]
        public void TestAllSuccess()
        {
            using (var stream = new MemoryStream())
            {
                ILogger logger = new StreamLogger(stream);
                string s = Util.GetMoreThan1000LongInput();
                string[] msgs = { "alpha", "beta", "gamma", s };
                foreach (var msg in msgs)
                {
                    Assert.IsTrue(logger.Info(msg));
                    Assert.IsTrue(logger.Debug(msg));
                    Assert.IsTrue(logger.Error(msg));
                }
            }
        }

        [TestMethod]
        public void TestAllUnSuccess()
        {
            using (var stream = new MemoryStream())
            {
                ILogger logger = new StreamLogger(stream);
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
}