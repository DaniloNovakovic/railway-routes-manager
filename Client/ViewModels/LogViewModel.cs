using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;
using Prism.Events;

namespace Client.ViewModels
{
    public class LogViewModel : ViewModelBase
    {
        public LogViewModel(ILogger logger, IEventAggregator eventAggregator) : base(logger, eventAggregator)
        {
            LogModels = new ObservableCollection<LogModel>();
        }

        public ObservableCollection<LogModel> LogModels { get; }

        public override async Task OnLoadedAsync()
        {
            string logText = await SafelyReadTextFromLogAsync();

            if (string.IsNullOrWhiteSpace(logText))
            {
                return;
            }

            LogModels.Clear();

            foreach (string line in SplitByNewline(logText))
            {
                if (LogModelParser.TryParse(line, out var logModel))
                {
                    LogModels.Add(logModel);
                }
            }
        }

        private static string[] SplitByNewline(string logText)
        {
            return logText.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            );
        }

        private async Task<string> SafelyReadTextFromLogAsync()
        {
            string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string path = Path.GetFullPath(Path.Combine(directory, "logs/client.log"));

            if (!File.Exists(path))
            {
                Logger.Warn($"Could not find log file on path '{path}'");
                return string.Empty;
            }

            try
            {
                return await Task.Run(() => File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.Message);
                EventAggregator.GetEvent<SnackbarMessageEvent>().Publish(ex.Message);
                return string.Empty;
            }
        }
    }
}