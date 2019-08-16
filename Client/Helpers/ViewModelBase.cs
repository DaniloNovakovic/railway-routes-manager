﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Prism.Commands;
using Prism.Mvvm;

namespace Client.Helpers
{
    public abstract class ViewModelBase : BindableBase
    {
        protected readonly ILogger Logger;

        protected ViewModelBase(ILogger logger = null)
        {
            Logger = logger;

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

                if (Logger != null)
                {
                    Logger.Exception(message);
                }
                else
                {
                    Trace.TraceError(message);
                }
            }
            finally
            {
                @finally?.Invoke();
            }
        }
    }
}