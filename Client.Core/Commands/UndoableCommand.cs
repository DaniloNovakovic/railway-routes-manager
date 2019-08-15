using System;

namespace Client.Core
{
    public class UndoableCommand : IUndoableCommand
    {
        private readonly Action _executeMethod;
        private readonly Action _undoMethod;

        public UndoableCommand(Action executeMethod, Action undoMethod)
        {
            _executeMethod = executeMethod;
            _undoMethod = undoMethod;
        }

        public void Execute()
        {
            _executeMethod?.Invoke();
        }

        public void UnExecute()
        {
            _undoMethod?.Invoke();
        }
    }
}