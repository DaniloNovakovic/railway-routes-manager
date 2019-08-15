using System.Collections.Generic;

namespace Client.Core
{
    public class CommandManager : ICommandManager
    {
        private readonly Stack<IUndoableCommand> _redoStack = new Stack<IUndoableCommand>();
        private readonly Stack<IUndoableCommand> _undoStack = new Stack<IUndoableCommand>();

        public void Execute(IUndoableCommand command)
        {
            command.Execute();
            _undoStack.Push(command);
        }

        public void Redo()
        {
            var command = _redoStack.Pop();
            command.Execute();
            _undoStack.Push(command);
        }

        public void Undo()
        {
            var command = _undoStack.Pop();
            command.UnExecute();
            _redoStack.Push(command);
        }
    }
}