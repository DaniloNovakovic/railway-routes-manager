using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Core
{
    public class CommandManager : ICommandManager
    {
        private readonly Stack<IUndoableCommand> _redoStack = new Stack<IUndoableCommand>();
        private readonly Stack<IUndoableCommand> _undoStack = new Stack<IUndoableCommand>();

        public event EventHandler CommandExecuted;

        public bool CanRedo()
        {
            return _redoStack.Count > 0;
        }

        public bool CanUndo()
        {
            return _undoStack.Count > 0;
        }

        public async Task ExecuteAsync(IUndoableCommand command)
        {
            await command.ExecuteAsync();
            _undoStack.Push(command);

            OnCommandExecuted();
        }

        public Task RedoAsync()
        {
            var command = _redoStack.Pop();
            return ExecuteAsync(command);
        }

        public async Task UndoAsync()
        {
            var command = _undoStack.Pop();
            await command.UnExecuteAsync();
            _redoStack.Push(command);

            OnCommandExecuted();
        }

        protected virtual void OnCommandExecuted()
        {
            CommandExecuted?.Invoke(this, EventArgs.Empty);
        }
    }
}