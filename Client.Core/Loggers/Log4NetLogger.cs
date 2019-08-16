using log4net;
using Prism.Logging;

namespace Client.Core
{
    public class Log4NetLogger : ILogger, ILoggerFacade
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(Log4NetLogger));

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Exception(string message)
        {
            _logger.Error(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        #region ILoggerFacade Members

        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    _logger.Debug(message);
                    break;

                case Category.Warn:
                    _logger.Warn(message);
                    break;

                case Category.Exception:
                    _logger.Error(message);
                    break;

                case Category.Info:
                    _logger.Info(message);
                    break;
            }
        }

        #endregion ILoggerFacade Members
    }
}