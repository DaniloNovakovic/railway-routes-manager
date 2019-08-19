using Client.Core;

namespace Client.Infrastructure
{
    public class AuthLoggerDecorator : ILogger
    {
        private readonly ILogger _logger;
        private readonly string _username;

        /// <summary>
        /// Constructs decorator that decorates `logger` with `username` attribute in each message
        /// </summary>
        /// <param name="logger">Object to decorate</param>
        /// <param name="username">Username to decorate with</param>
        public AuthLoggerDecorator(ILogger logger, string username)
        {
            _logger = logger;
            _username = username;
        }

        public void Debug(string message)
        {
            _logger.Debug(FormatMessage(message));
        }

        public void Exception(string message)
        {
            _logger.Exception(FormatMessage(message));
        }

        public void Info(string message)
        {
            _logger.Info(FormatMessage(message));
        }

        public void Warn(string message)
        {
            _logger.Warn(FormatMessage(message));
        }

        private string FormatMessage(string message)
        {
            return $"{_username}: {message}";
        }
    }
}