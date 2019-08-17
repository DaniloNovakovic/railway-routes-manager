using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Client.Core
{
    public static class LogModelParser
    {
        /// <summary>
        /// Parses message that is expected to be in format '%date %level - %message%newline'
        /// </summary>
        public static LogModel Parse(string line)
        {
            var match = Regex.Match(line, @"\s*?([0-9/\:,-]+?\s+[0-9/\:,-]+?)\s+?(\S+?) - (.+)\s*$");
            return new LogModel()
            {
                Date = match.Groups[1].Value,
                Level = match.Groups[2].Value,
                Message = match.Groups[3].Value
            };
        }

        public static bool TryParse(string line, out LogModel logModel)
        {
            try
            {
                logModel = Parse(line);
                return IsValid(logModel);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                logModel = new LogModel();
                return false;
            }
        }

        private static bool IsValid(LogModel logModel)
        {
            return !string.IsNullOrWhiteSpace(logModel.Date) && !string.IsNullOrWhiteSpace(logModel.Level);
        }
    }
}