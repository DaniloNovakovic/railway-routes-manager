using Xunit;

namespace Client.Core.Tests
{
    public class LogModelParserTests
    {
        [Fact]
        public void ParseTest()
        {
            const string line = "2019-08-17 15:56:00,282 INFO - admin's role: Administrator";

            var result = LogModelParser.Parse(line);

            Assert.Equal("2019-08-17 15:56:00,282", result.Date.Trim());
            Assert.Equal("INFO", result.Level.Trim());
            Assert.Equal("admin's role: Administrator", result.Message.Trim());
        }

        [Fact]
        public void TryParseTest_LineIsInvalid_ReturnFalse()
        {
            const string line = " admin's role: Administrator - INFO -";

            bool result = LogModelParser.TryParse(line, out var logModel);

            Assert.False(result);
        }

        [Fact]
        public void TryParseTest_LineIsValid_ReturnTrue()
        {
            const string line = "2019-08-17 15:56:00,282 INFO - admin's role: Administrator";

            bool result = LogModelParser.TryParse(line, out var logModel);

            Assert.True(result);
        }
    }
}