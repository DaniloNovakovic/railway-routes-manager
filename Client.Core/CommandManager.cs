using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Core
{
    public class CommandManager : ICommandManager
    {
        private readonly Stack<IUndoableCommand> _redoStack = new Stack<IUndoableCommand>();
        private readonly Stack<IUndoableCommand> _undoStack = new Stack<IUndoableCommand>();

        public async Task ExecuteAsync(IUndoableCommand command)
        {
            await command.ExecuteAsync();
            _undoStack.Push(command);
        }

        public async Task RedoAsync()
        {
            var command = _redoStack.Pop();
            await command.ExecuteAsync();
            _undoStack.Push(command);
        }

        public async Task UndoAsync()
        {
            var command = _undoStack.Pop();
            await command.UnExecuteAsync();
            _redoStack.Push(command);
        }
    }
}