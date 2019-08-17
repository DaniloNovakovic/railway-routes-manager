using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;

namespace Client.ViewModels
{
    public class LogViewModel : ViewModelBase
    {
        private string _logText;

        public LogViewModel(ILogger logger) : base(logger)
        {
        }

        public string LogText
        {
            get { return _logText; }
            set { SetProperty(ref _logText, value); }
        }

        public override Task OnLoadedAsync()
        {
            string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string path = Path.GetFullPath(Path.Combine(directory, "logs/client.log"));

            if (!File.Exists(path))
            {
                LogText = $"Could not find log file on path '{path}'";
                Logger.Warn(LogText);
                return Task.CompletedTask;
            }

            ReadAllText(path);

            return Task.CompletedTask;
        }

        private void ReadAllText(string path)
        {
            try
            {
                LogText = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                LogText = ex.Message;
                Logger.Exception(LogText);
            }
        }
    }
}