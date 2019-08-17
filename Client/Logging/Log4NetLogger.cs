using System.Reflection;
using Client.Core;
using log4net;
using Prism.Logging;

namespace Client
{
    public class Log4NetLogger : ILogger, ILoggerFacade
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        public void Exception(string message)
        {
            Logger.Error(message);
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }

        public void Warn(string message)
        {
            Logger.Warn(message);
        }

        #region ILoggerFacade Members

        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    Logger.Debug(message);
                    break;

                case Category.Warn:
                    Logger.Warn(message);
                    break;

                case Category.Exception:
                    Logger.Error(message);
                    break;

                case Category.Info:
                    Logger.Info(message);
                    break;
            }
        }

        #endregion ILoggerFacade Members
    }
}