namespace Logger.Tests
{
    [TestClass()]
    public class StreamLoggerAsyncTests
    {
        [TestMethod]
        public void TestAllSuccess()
        {
            using (var stream = new MemoryStream())
            {
                var logger = new StreamLoggerAsync(stream);
                string s = Util.GetMoreThan1000LongInput();
                string[] msgs = { "alpha", "beta", "gamma", s };
                foreach (var msg in msgs)
                {
                    Assert.IsTrue(logger.Info(msg).GetAwaiter().GetResult());
                    Assert.IsTrue(logger.Debug(msg).GetAwaiter().GetResult());
                    Assert.IsTrue(logger.Error(msg).GetAwaiter().GetResult());
                }
            }
        }

        [TestMethod]
        public void TestAllUnSuccess()
        {
            using (var stream = new MemoryStream())
            {
                var logger = new StreamLoggerAsync(stream);
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
}