using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Client.Helpers
{
    public abstract class ViewModelBase : BindableBase
    {
        protected readonly IEventAggregator EventAggregator;
        protected readonly ILogger Logger;

        protected ViewModelBase(ILogger logger = null, IEventAggregator eventAggregator = null)
        {
            Logger = logger;
            EventAggregator = eventAggregator;

            OnLoadedCommand = new DelegateCommand(async () => await OnLoadedAsync());
        }

        public ICommand OnLoadedCommand { get; }

        /// <summary>
        /// Fires when OnLoadedCommand is executed
        /// </summary>
        public virtual Task OnLoadedAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Safely executes @try function asynchronously | Wraps it in try/catch/finally block
        /// </summary>
        /// <param name="try">Function to attempt to execute</param>
        /// <param name="finally">
        /// Action to perform after try/catch | Note: Don't perform async requests here
        /// </param>
        protected virtual async Task SafeExecuteAsync(Func<Task> @try, Action @finally = null)
        {
            try
            {
                await @try?.Invoke();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException?.Message ?? ex.Message;
                OnError(message);
            }
            finally
            {
                @finally?.Invoke();
            }
        }

        private void OnError(string message)
        {
            if (Logger != null)
            {
                Logger.Exception(message);
            }
            else
            {
                Trace.TraceError(message);
            }

            if (EventAggregator != null)
            {
                EventAggregator.GetEvent<SnackbarMessageEvent>().Publish(message);
            }
        }
    }
}