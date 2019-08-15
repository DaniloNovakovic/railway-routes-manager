using System;
using System.Threading.Tasks;

namespace Client.Core
{
    public class UndoableCommand : IUndoableCommand
    {
        private readonly Func<Task> _executeMethod;
        private readonly Func<Task> _undoMethod;

        public UndoableCommand(Func<Task> executeMethod, Func<Task> undoMethod)
        {
            _executeMethod = executeMethod;
            _undoMethod = undoMethod;
        }

        public Task ExecuteAsync()
        {
            return _executeMethod?.Invoke();
        }

        public Task UnExecuteAsync()
        {
            return _undoMethod?.Invoke();
        }
    }
}